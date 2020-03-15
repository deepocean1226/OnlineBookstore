+ 修改appsettings.json中的DefaultConnection为自己的本地数据库
+ 在程序包管理控制台执行Add-Migration InitialCreate
+ 再执行Update-Database可完成本地数据库迁移建表
+ 在默认index页的构造器中注入了数据库上下文服务，完成了一次插入操作，可以用来测试
