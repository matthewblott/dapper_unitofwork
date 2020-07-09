namespace dapper_unitofwork.Repositories.Implementations
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Linq;
  using System.Reflection;
  using System.Text;
  using Dapper;
  using Humanizer;
  using Interfaces;

  public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
  {
    protected IDbTransaction Transaction { get; private set; }
    protected IDbConnection Connection => Transaction.Connection;
    
    private static string TypeName => typeof(T).Name;
    private static string TableName => TypeName.Pluralize();
    private static string IdFieldName => $"{TypeName}Id";
    private static bool HasIdentityField => 
      !typeof(T).GetProperties().Any(p => p.Name == IdFieldName && p.PropertyType == typeof(string));

    private static object GetIdFieldValue(T entity) => typeof(T).GetProperties()
        .Single(p => p.Name == IdFieldName).GetValue(entity);

    protected RepositoryBase(IDbTransaction transaction) => Transaction = transaction;

    public IEnumerable<T> All() => Connection.Query<T>($"select * from {TableName} ");

    public T Find(object id) 
      => Connection.Query<T>($"select * from {TableName} where {IdFieldName} = @id", 
        new { id }, Transaction).FirstOrDefault();

    public object Add(T entity)
    {
      // Include Id field if it isn't an auto generated field
      var parameters = GetParams(GetProperties(), entity);
      var sql = CreateInsertSql(GetProperties(HasIdentityField));
      var rowId = Connection.QuerySingle<int>(sql, parameters, Transaction);
      
      // To avoid confusion the Id field's value is returned for non identity fields
      return HasIdentityField ? rowId : GetIdFieldValue(entity);
    }
    
    public void Update(T entity)
    {
      var sql = CreateUpdateSql();
      var value = GetProperties().Single(p => p.Name == IdFieldName).GetValue(entity);
      var parameters = GetParams(GetProperties(true), entity);

      parameters.Add("@id", value);
      
      Connection.Execute(sql, parameters, Transaction);
      
    }

    public void Remove(object id) =>
      Connection.Execute($"delete from {TableName} where {IdFieldName} = @id ",
        new { id }, Transaction);

    private static IEnumerable<PropertyInfo> GetProperties(bool excludeIdField = false)
    {
      var q = 
        from p in typeof(T).GetProperties()
        where
          p.PropertyType == typeof(bool) ||
          p.PropertyType == typeof(object) ||
          p.PropertyType == typeof(string) ||
          p.PropertyType == typeof(int) ||
          p.PropertyType == typeof(decimal) ||
          p.PropertyType == typeof(double) ||
          p.PropertyType == typeof(Enum) ||
          p.PropertyType == typeof(DateTime)
        select p;

      var properties = q.ToList();

      return excludeIdField ? properties.Where(p => p.Name != $"{TypeName}Id") : properties;
      
    }

    private static DynamicParameters GetParams(IEnumerable<PropertyInfo> properties, object entity)
    {
      var parameters = new DynamicParameters();
      
      foreach (var p in properties)
      {
        var value = p.GetValue(entity);
        var parameterName = $"@{p.Name}";
        var parameterValue = value;
        
        parameters.Add(parameterName, parameterValue);

      }

      return parameters;

    }

    private static string CreateInsertSql(IEnumerable<PropertyInfo> properties)
    {
      var builder = new StringBuilder();
      
      foreach (var property in properties)
      {
        builder.Append("@" + property.Name + ",");
      }
      
      var valuesSql = $"({builder.Remove(builder.Length - 1, 1)})"; // Remove trailing comma
      var fieldsSql = valuesSql.Replace("@", string.Empty);
      var sql = $"insert into {TableName} {fieldsSql} values {valuesSql}; select last_insert_rowid();";
      
      return sql;
    }

    private static string CreateUpdateSql()
    {
      var builder = new StringBuilder();
      var properties = GetProperties(true);

      foreach (var property in properties)
      {
        builder.Append(property.Name + "=@" + property.Name + ",");
      }
      
      var fieldsSql = builder.Remove(builder.Length - 1, 1); // Remove trailing comma
      var sql = $"update {TableName} set {fieldsSql} where {IdFieldName} = @id";
      
      return sql;
    }

  }
  
}