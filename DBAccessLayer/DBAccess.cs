using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBAccessLayer
{
     /// </summary>
     /// <typeparam name="Connection">数据库连接数据类型</typeparam>
     /// <typeparam name="Command">数据库命令数据类型</typeparam>
     /// <typeparam name="DataAdapter">数据库数据适配器数据类型</typeparam>
    class DBAccess<Connection, Command, DataAdapter> : IDisposable,IDBAccess
         where Connection : IDbConnection, new()
         where Command : IDbCommand, new()
         where DataAdapter : IDbDataAdapter, new()
     {

         private Connection conn;
 
         /// <summary>
         /// 连接字符串的有参构造方法
         /// </summary>
         /// <param name="connectionString">指定的连接字符串</param>
         public DBAccess(string connectionString)
         {
             this.conn = new Connection();
             this.conn.ConnectionString = connectionString + ";MultipleActiveResultSets=true";
         }
         public void Dispose()
         {
             this.conn.Close();
         }
        #region 打开数据库连接的方法
         /// <summary>
         /// 打开数据库连接的方法
         /// </summary>
         /// <returns>bIsSucceed(true:打开数据库连接成功 false:打开数据库连接失败)</returns>
         public bool OpenConnection()
         {
             bool bIsSucceed = true;
             try
             {   
                 // 判断数据库连接是否打开，没有打开则打开数据库
                 if (this.conn.State != ConnectionState.Open)
                 {
                     this.conn.Open();
                 }
             }
             catch (Exception exp)
             {
                 bIsSucceed = false;
                 throw new Exception("打开数据库连接错误! 信息:" + exp.Message);
             }
 
            return bIsSucceed;
         }
         #endregion
 
        #region 关闭数据库连接的方法
         /// <summary>
         /// 关闭数据库连接的方法
         /// </summary>
         /// <returns>bIsSucceed(true:关闭数据库连接成功 false:关闭数据库连接失败)</returns>
         public bool CloseConnection()
         {
             bool bIsSucceed = true;
             try
             {   // 判断数据库连接是否关闭，没有关闭则关闭数据库
                 if (this.conn.State != ConnectionState.Closed)
                 {
                     this.conn.Close();
                 }
             }
             catch (Exception exp)
             {
                 bIsSucceed = false;
                 throw new Exception("关闭数据库连接错误! 信息:" + exp.Message);
             }
 
            return bIsSucceed;
         }
         #endregion
 
        #region 创建一个Command对象
         /// <summary>
         /// 创建一个Command对象的方法(指定其CommandText, CommandType, Connection，有参数则添加参数)
         /// </summary>
         /// <param name="CommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回命令对象(cmd)</returns>
         private Command CreateCommand(string commandText, CommandType type, IDataParameter[] paraList)
         {
             Command cmd = new Command();
             cmd.CommandText = commandText;
             cmd.CommandType = type;
             cmd.Connection = this.conn;
             
            // 有参数则添加参数
             if (paraList != null)
             {
                 foreach (IDataParameter para in paraList)
                 {
                     cmd.Parameters.Add(para);
                 }
             }
 
            return cmd;
         }
         #endregion

        #region 执行ExecuteNonQuery的SQL语句
         /// <summary>
         /// 执行一条Sql语句
         /// </summary>
         /// <returns>返回影响的行数(row)</returns>
         public int ExecuteNonQuerySql(string commandText)
         {
             int row = -1;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(commandText,CommandType.Text, null);

                 // 2.打开数据库连接
                 this.OpenConnection();

                 // 3.执行命令
                 row = cmd.ExecuteNonQuery();
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回影响的行数错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             /* 
               * 另一种写法,可以将 return row; 放在try语句块的末尾.
               * finally 程序块成功执行后,然后在执行 return 语句.
               */
             // 5.返回影响的行数
             return row;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回影响的行数的方法(适合执行Update、Delete、Insert三种T_SQL语句)
         /// </summary>
         /// <param name="CommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回影响的行数(row)</returns>
         public int ExecuteNonQuerySql(string commandText, IDataParameter[] paraList)
         {
             int row = -1;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(commandText, CommandType.Text, paraList);

                 // 2.打开数据库连接
                 this.OpenConnection();

                 // 3.执行命令
                 row = cmd.ExecuteNonQuery();
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回影响的行数错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             /* 
               * 另一种写法,可以将 return row; 放在try语句块的末尾.
               * finally 程序块成功执行后,然后在执行 return 语句.
               */
             // 5.返回影响的行数
             return row;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回影响的行数的方法(适合执行Update、Delete、Insert三种T_SQL语句)
         /// </summary>
         /// <param name="CommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回影响的行数(row)</returns>
         public int ExecuteNonQuery(string commandText, CommandType type, IDataParameter[] paraList)
         {
             int row = -1;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(commandText, type, paraList);
 
                // 2.打开数据库连接
                 this.OpenConnection();
 
                // 3.执行命令
                 row = cmd.ExecuteNonQuery();
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回影响的行数错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }
 
            /* 
              * 另一种写法,可以将 return row; 放在try语句块的末尾.
              * finally 程序块成功执行后,然后在执行 return 语句.
              */
             // 5.返回影响的行数
             return row;
         }
         #endregion

        #region 执行ExecuteNonQuery的存储过程
         /// <summary>
         /// 执行一条Sql语句
         /// </summary>
         /// <returns>返回影响的行数(row)</returns>
         public int ExecuteNonQueryProc(string procedureName)
         {
             int row = -1;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(procedureName, CommandType.StoredProcedure, null);

                 // 2.打开数据库连接
                 this.OpenConnection();
                 
                 // 3.执行命令
                 row = cmd.ExecuteNonQuery();
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回影响的行数错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             /* 
               * 另一种写法,可以将 return row; 放在try语句块的末尾.
               * finally 程序块成功执行后,然后在执行 return 语句.
               */
             // 5.返回影响的行数
             return row;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回影响的行数的方法(适合执行Update、Delete、Insert三种T_SQL语句)
         /// </summary>
         /// <param name="CommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回影响的行数(row)</returns>
         public int ExecuteNonQueryProc(string procedureName, IDataParameter[] paraList)
         {
             int row = -1;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(procedureName, CommandType.StoredProcedure, paraList);

                 // 2.打开数据库连接
                 this.OpenConnection();

                 // 3.执行命令
                 row = cmd.ExecuteNonQuery();
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回影响的行数错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             /* 
               * 另一种写法,可以将 return row; 放在try语句块的末尾.
               * finally 程序块成功执行后,然后在执行 return 语句.
               */
             // 5.返回影响的行数
             return row;
         }
         
         #endregion
 
        #region 执行SQL语句返回首行首列的方法
         /// <summary>
         /// 执行一条T_SQL命令返回首行首列的方法(适合执行Select命令中含有聚合函数的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <returns>返回首行首列(result)</returns>
         public object ExecuteScalarSql(string selectcommandText)
         {
             object result = null;
             IDbTransaction myTran = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, CommandType.Text, null);

                 // 2.打开数据库连接
                 this.OpenConnection();
                 myTran = this.conn.BeginTransaction();
                 cmd.Transaction = myTran;
                 // 3.执行命令
                 result = cmd.ExecuteScalar();
                 cmd.Transaction.Commit();
             }
             catch (Exception exp)
             {
                 myTran.Rollback();
                 throw new Exception("执行T_SQL命令返回首行首列错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回首行首列
             return result;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回首行首列的方法
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回首行首列(result)</returns>
         public object ExecuteScalarSql(string selectcommandText, IDataParameter[] paraList)
         {
             object result = null;
             IDbTransaction myTran = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, CommandType.Text, paraList);

                 // 2.打开数据库连接
                 this.OpenConnection();
                 myTran = this.conn.BeginTransaction();
                 cmd.Transaction = myTran;
                 // 3.执行命令
                 result = cmd.ExecuteScalar();
                 cmd.Transaction.Commit();
             }
             catch (Exception exp)
             {
                 myTran.Rollback();
                 throw new Exception("执行T_SQL命令返回首行首列错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回首行首列
             return result;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回首行首列的方法(适合执行Select命令中含有聚合函数的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回首行首列(result)</returns>
         public object ExecuteScalar(string selectcommandText, CommandType type, IDataParameter[] paraList)
         {
             object result = null;
             IDbTransaction myTran = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, type, paraList);
                 
                 // 2.打开数据库连接
                 this.OpenConnection();
                 myTran = this.conn.BeginTransaction();
                 cmd.Transaction = myTran;
                 // 3.执行命令
                 result = cmd.ExecuteScalar();
                 cmd.Transaction.Commit();
             }
             catch (Exception exp)
             {
                 myTran.Rollback();
                 throw new Exception("执行T_SQL命令返回首行首列错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回首行首列
             return result;
         }
         #endregion

        #region 执行存储过程返回首行首列的方法
         /// <summary>
         /// 执行一条T_SQL命令返回首行首列的方法(适合执行Select命令中含有聚合函数的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <returns>返回首行首列(result)</returns>
         public object ExecuteScalarProc(string procedureName)
         {
             object result = null;
             IDbTransaction myTran = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(procedureName, CommandType.StoredProcedure, null);

                 // 2.打开数据库连接
                 this.OpenConnection();
                 myTran = this.conn.BeginTransaction();
                 cmd.Transaction = myTran;
                 // 3.执行命令
                 result = cmd.ExecuteScalar();
                 cmd.Transaction.Commit();
             }
             catch (Exception exp)
             {
                 myTran.Rollback();
                 throw new Exception("执行T_SQL命令返回首行首列错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回首行首列
             return result;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回首行首列的方法
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回首行首列(result)</returns>
         public object ExecuteScalarProc(string procedureName, IDataParameter[] paraList)
         {
             object result = null;
             IDbTransaction myTran = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(procedureName, CommandType.StoredProcedure, paraList);

                 // 2.打开数据库连接
                 this.OpenConnection();
                 myTran = this.conn.BeginTransaction();
                 cmd.Transaction = myTran;
                 // 3.执行命令
                 result = cmd.ExecuteScalar();
                 cmd.Transaction.Commit();
             }
             catch (Exception exp)
             {
                 myTran.Rollback();
                 throw new Exception("执行T_SQL命令返回首行首列错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回首行首列
             return result;
         }
         #endregion
 
        #region 执行SQL语句返回IDataReader的方法
         /// <summary>
         /// 执行一条T_SQL命令返回IDataReader的方法(适合连接模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         
         /// <returns>返回数据读取器(reader)</returns>
         public IDataReader ExecuteReaderSql(string selectcommandText)
         {
             IDataReader reader = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, CommandType.Text, null);

                 // 2.打开数据库连接
                 this.OpenConnection();

                 // 3.执行命令
                 reader = cmd.ExecuteReader();
             }
             catch (Exception exp)
             {
                 // 如果出现异常，在连接模式情况下应该先关闭数据库，在抛出异常
                 this.CloseConnection();
                 throw new Exception("执行T_SQL命令返回DataReader错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回数据读取器
             return reader;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回IDataReader的方法(适合连接模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回数据读取器(reader)</returns>
         public IDataReader ExecuteReaderSql(string selectcommandText, IDataParameter[] paraList)
         {
             IDataReader reader = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, CommandType.Text, paraList);

                 // 2.打开数据库连接
                 this.OpenConnection();

                 // 3.执行命令
                 reader = cmd.ExecuteReader();
             }
             catch (Exception exp)
             {
                 // 如果出现异常，在连接模式情况下应该先关闭数据库，在抛出异常
                 this.CloseConnection();
                 throw new Exception("执行T_SQL命令返回DataReader错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回数据读取器
             return reader;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回IDataReader的方法(适合连接模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回数据读取器(reader)</returns>
         public IDataReader ExecuteReader(string selectcommandText, CommandType type, IDataParameter[] paraList)
         {
             IDataReader reader = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, type, paraList);
 
                // 2.打开数据库连接
                 this.OpenConnection();
 
                // 3.执行命令
                 reader = cmd.ExecuteReader();
             }
             catch (Exception exp)
             {
                 // 如果出现异常，在连接模式情况下应该先关闭数据库，在抛出异常
                 this.CloseConnection();
                 throw new Exception("执行T_SQL命令返回DataReader错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }
 
            // 5.返回数据读取器
             return reader;
         }
         #endregion

        #region 执行存储过程返回IDataReader的方法
         /// <summary>
         /// 执行一条T_SQL命令返回IDataReader的方法(适合连接模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>

         /// <returns>返回数据读取器(reader)</returns>
         public IDataReader ExecuteReaderProc(string procedureName)
         {
             IDataReader reader = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(procedureName, CommandType.StoredProcedure, null);

                 // 2.打开数据库连接
                 this.OpenConnection();

                 // 3.执行命令
                 reader = cmd.ExecuteReader();
             }
             catch (Exception exp)
             {
                 // 如果出现异常，在连接模式情况下应该先关闭数据库，在抛出异常
                 this.CloseConnection();
                 throw new Exception("执行T_SQL命令返回DataReader错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回数据读取器
             return reader;
         }
         /// <summary>
         /// 执行一条T_SQL命令返回IDataReader的方法(适合连接模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回数据读取器(reader)</returns>
         public IDataReader ExecuteReaderProc(string procedureName, IDataParameter[] paraList)
         {
             IDataReader reader = null;
             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(procedureName, CommandType.StoredProcedure, paraList);

                 // 2.打开数据库连接
                 if(this.conn.State == ConnectionState.Closed)
                    this.OpenConnection();

                 // 3.执行命令
                 reader = cmd.ExecuteReader();
             }
             catch (Exception exp)
             {
                 // 如果出现异常，在连接模式情况下应该先关闭数据库，在抛出异常
                 this.CloseConnection();
                 throw new Exception("执行T_SQL命令返回DataReader错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 //this.CloseConnection();
             }

             // 5.返回数据读取器
             return reader;
         }
         
         #endregion
 
        #region 执行一条T_SQL命令返回DataSet的方法
         /// <summary>
         /// 执行一条T_SQL命令返回DataSet的方法(适合断开模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <returns>返回数据记录集(ds)</returns> 
         public DataSet ExecuteDataSetSql(string selectcommandText)
         {
             DataSet data = new DataSet();

             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, CommandType.Text, null);

                 // 2.创建一个数据适配器
                 DataAdapter adapter = new DataAdapter();
                 adapter.SelectCommand = cmd;
                 // DataAdapter da = new DataAdapter(cmd);等价于上面的两行代码

                 // 3.利用数据适配器填充数据记录集
                 adapter.Fill(data);
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回DataSet错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回数据记录集
             return data;

         }
         /// <summary>
         /// 执行一条T_SQL命令返回DataSet的方法(适合断开模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回数据记录集(ds)</returns> 
         public DataSet ExecuteDataSetSql(string selectcommandText, IDataParameter[] paraList)
         {
             DataSet data = new DataSet();

             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, CommandType.Text, paraList);

                 // 2.创建一个数据适配器
                 DataAdapter adapter = new DataAdapter();
                 adapter.SelectCommand = cmd;
                 // DataAdapter da = new DataAdapter(cmd);等价于上面的两行代码

                 // 3.利用数据适配器填充数据记录集
                 adapter.Fill(data);
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回DataSet错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回数据记录集
             return data;

         }
         /// <summary>
         /// 执行一条T_SQL命令返回DataSet的方法(适合断开模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="type">T_SQL命令的类型(文本类型:Text或存储过程类型:StoredProcedure)</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回数据记录集(ds)</returns> 
         public DataSet ExecuteDataSet(string selectcommandText, CommandType type, IDataParameter[] paraList)
         {
             DataSet data = new DataSet();
 
            try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(selectcommandText, type, paraList);
 
                // 2.创建一个数据适配器
                 DataAdapter adapter = new DataAdapter();
                 adapter.SelectCommand = cmd;
                 // DataAdapter da = new DataAdapter(cmd);等价于上面的两行代码
 
                // 3.利用数据适配器填充数据记录集
                 adapter.Fill(data); 
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回DataSet错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }
 
            // 5.返回数据记录集
             return data;
 
        }
         #endregion

        #region 执行存储过程命令返回DataSet的方法
         /// <summary>
         /// 执行一条T_SQL命令返回DataSet的方法(适合断开模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <returns>返回数据记录集(ds)</returns> 
         public DataSet ExecuteDataSetProc(string procedureName)
         {
             DataSet data = new DataSet();

             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(procedureName, CommandType.StoredProcedure, null);

                 // 2.创建一个数据适配器
                 DataAdapter adapter = new DataAdapter();
                 adapter.SelectCommand = cmd;
                 // DataAdapter da = new DataAdapter(cmd);等价于上面的两行代码

                 // 3.利用数据适配器填充数据记录集
                 adapter.Fill(data);
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回DataSet错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回数据记录集
             return data;

         }
         /// <summary>
         /// 执行一条T_SQL命令返回DataSet的方法(适合断开模式执行Select命令的T_SQL语句)
         /// </summary>
         /// <param name="selectcommandText">T_SQL命令字符串或存储过程名</param>
         /// <param name="paraList">T_SQL命令的参数集合(无参数添加时即为null)</param>
         /// <returns>返回数据记录集(ds)</returns> 
         public DataSet ExecuteDataSetProc(string procedureName, IDataParameter[] paraList)
         {
             DataSet data = new DataSet();

             try
             {
                 // 1.创建一个Command对象，指定其commandText, CommandType, Connection，有参数则添加参数
                 Command cmd = CreateCommand(procedureName, CommandType.StoredProcedure, paraList);

                 // 2.创建一个数据适配器
                 DataAdapter adapter = new DataAdapter();
                 adapter.SelectCommand = cmd;
                 // DataAdapter da = new DataAdapter(cmd);等价于上面的两行代码

                 // 3.利用数据适配器填充数据记录集
                 adapter.Fill(data);
             }
             catch (Exception exp)
             {
                 throw new Exception("执行T_SQL命令返回DataSet错误! 信息:" + exp.Message);
             }
             finally
             {
                 // 4.关闭数据库连接
                 this.CloseConnection();
             }

             // 5.返回数据记录集
             return data;

         }
         
         #endregion
         
     }
 }