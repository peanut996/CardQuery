# CardQuery 校园一卡通记录查询系统

##  实现  
+  C# WPF
+  SQL Server Express  2012 
+  Visual Studio 2017 及其组件(SQL Server 2016 local DB, Blend For Visual Studio 2017)  

## 简述
本WPF程序实现了数据库的简单的查、插、删、改，以及拥有普通用户和高级用户的权限区别  
普通用户：ReadOnly  
高级用户：Read and Write  
整体结构分为`MainWindow`、`RecordWindow`、`Superwindow`三个继承`Window`类
在`RecordWindow`上实现不同的转型  
中文开关没有实现，影响参数传递，工作量较大



