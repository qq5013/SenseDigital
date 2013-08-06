using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBAccessLayer
{
    public interface IDBAccess:IDisposable
     {

         /// <summary>
         /// 打开数据库连接的方法
         /// </summary>
         bool OpenConnection(); 

         /// <summary>
         /// 关闭数据库连接的方法
         /// </summary>
         bool CloseConnection();

 
        #region 执行一条T_SQL命令返回影响的行数的方法(适合执行Update、Delete、Insert三种T_SQL语句)
         /// <summary>
         /// 执行一条T_SQL命令返回影响的行数的方法(适合执行Update、Delete、Insert三种T_SQL语句)
         /// </summary>
         /// <param name="CommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回影响的行数(row)</returns>
         int ExecuteNonQuerySql(string commandText);
         int ExecuteNonQuerySql(string commandText, IDataParameter[] paraList);
         int ExecuteNonQuery(string commandText, CommandType type, IDataParameter[] paraList);

         int ExecuteNonQueryProc(string procedureName);
         int ExecuteNonQueryProc(string procedureName, IDataParameter[] paraList);

         #endregion
 
        #region 执行一条T_SQL命令返回首行首列的方法(适合执行Select命令中含有聚合函数的T_SQL语句)
         /// <summary>
         /// 执行一条T_SQL命令返回首行首列的方法(适合执行Select命令中含有聚合函数的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回首行首列(obj)</returns>
         object ExecuteScalarSql(string selectcommandText);
         object ExecuteScalarSql(string selectcommandText, IDataParameter[] paraList);
         object ExecuteScalar(string selectcommandText, CommandType type, IDataParameter[] paraList);
         object ExecuteScalarProc(string procedureName);
         object ExecuteScalarProc(string procedureName, IDataParameter[] paraList);
         #endregion
 
        #region 执行一条T_SQL命令返回IDataReader的方法(适合连接模式执行Select命令的T_SQL语句)
         /// <summary>
         /// 执行一条T_SQL命令返回IDataReader的方法(适合连接模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回数据读取器(reader)</returns>
         IDataReader ExecuteReaderSql(string selectcommandText);
         IDataReader ExecuteReaderSql(string selectcommandText, IDataParameter[] paraList);
         IDataReader ExecuteReader(string selectcommandText, CommandType type, IDataParameter[] paraList);
         IDataReader ExecuteReaderProc(string procedureName);
         IDataReader ExecuteReaderProc(string procedureName, IDataParameter[] paraList);
         #endregion
 
        #region 执行一条T_SQL命令返回DataSet的方法(适合断开模式执行Select命令的T_SQL语句)
         /// <summary>
         /// 执行一条T_SQL命令返回DataSet的方法(适合断开模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回数据记录集(ds)</returns> 
         DataSet ExecuteDataSetSql(string selectcommandText);
         DataSet ExecuteDataSetSql(string selectcommandText, IDataParameter[] paraList);
         DataSet ExecuteDataSet(string selectcommandText, CommandType type, IDataParameter[] paraList);
         DataSet ExecuteDataSetProc(string procedureName);
         DataSet ExecuteDataSetProc(string procedureName, IDataParameter[] paraList);
         #endregion
     }
}
 
