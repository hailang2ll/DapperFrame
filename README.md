# DapperFrame
1、DapperFrame框架，支持netstandard2.0/net45  <br />
2、DapperFrame一个ORM框架，在Dapper原生代码上进行了扩展，目前支持netstandard2.0/net45框架，里面有实例过程，操作也方便简单，后续会一直更新，也会支持千库千表的操作，希望大家也可以提出更好的建议与想法   <br />
3、框架说明：  <br />
	DapperFrame为核心ORM框架，只需要引用它就可以生成对应的框架版本，后续会不停的扩展  <br />
	DapperWeb为.net core2.0项目，里面有实例过程  <br />
	DapperWebAPI为.net framework4.5项目，里面有实例过程  <br />
	其它都为相关类，可以参考  <br />
  <br />
欢迎大家提出更好的建议，我会第一时间进行思考与调整！  <br />

一、原生态的Dapper语法

1、集合语法

var list = connection.Query<Sys_DapperTest>("SELECT * FROM dbo.Sys_DapperTest ORDER BY ID DESC").ToList();

var list = connection.Query<Sys_DapperTest>("SELECT * FROM dbo.Sys_DapperTest WHERE ID IN @Ids", new { Ids = new int[] { 1, 2, 3, 4 } }).ToList();
  
var list = connection.Query<Sys_DapperTest>("SELECT * FROM dbo.Sys_DapperTest WHERE Name LIKE @Name", new { Name = "%hai%" }).ToList();
  
var list = connection.Query<Sys_DapperTest>("SELECT * FROM dbo.Sys_DapperTest WHERE ID=@ID", new { ID = 13 }).First();

2、新增语法

var flag = connection.Execute("INSERT dbo.Sys_DapperTest( Name, Age )VALUES  ( @Name,@Age)", new { Name = "jiayou", Age = 2 });

Sys_DapperTest entity = new Sys_DapperTest()
{
Name = "aaaa",
Age = 3,
};
var flag = connection.Execute("INSERT dbo.Sys_DapperTest( Name, Age )VALUES  ( @Name,@Age)", entity);

3、删除语法

var flag = connection.Execute("DELETE FROM dbo.Sys_DapperTest WHERE ID=@ID", new { ID = 14 });

4、修改语法

var flag = connection.Execute("UPDATE dbo.Sys_DapperTest SET Name=@Name WHERE ID=@ID", new { Name = "bbbb", ID = 15 });

5、事物语法

using (var transaction = connection.BeginTransaction())
{
var flag = connection.Execute("INSERT dbo.Sys_DapperTest( Name, Age )VALUES  ( N'hailangCCC',1)", transaction: transaction);
connection.Execute("insert into Sys_DapperProduct ([ProductName], [ProductCount]) values (N'ABC'+" + 1 + ", '1');", transaction: transaction);
transaction.Commit();
}
 

二、DapperExtensions语法

1、集合

BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();
var list = db.GetList();

2、分页

 BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();
//条件

var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
pg.Predicates.Add(Predicates.Field<Sys_DapperTest>(x => x.ID, Operator.Eq, new List<int>() { 1, 2, 3, 4 }));
//排序
	
List<ISort> sort = new List<ISort>();
sort.Add(Predicates.Sort<Sys_DapperTest>(p => p.ID, true));
var list = db.GetPageList(pg, sort, 1, 10).ToList();
int total = db.Count();
	
3、实体

BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();
Sys_DapperTest entity = db.Get(15);

4、新增

Sys_DapperTest entity = new Sys_DapperTest()
{
Name = "lang",
Age = 3,
};
BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();
bool flag = db.Insert(entity);

5、修改

bool flag = db.Update(entity);
   
