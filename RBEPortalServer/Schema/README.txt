Check this link to know how to use the Include method WITHOUT using strings.

http://msdn.microsoft.com/en-us/library/gg671236%28v=vs.103%29.aspx

The path expression must be composed of simple property access expressions together with calls to Select in order to compose additional includes after including a collection property. Examples of possible include paths are:

    To include a single reference: query.Include(e => e.Level1Reference).

    To include a single collection: query.Include(e => e.Level1Collection).

    To include a reference and then a reference one level down: query.Include(e => e.Level1Reference.Level2Reference).

    To include a reference and then a collection one level down: query.Include(e => e.Level1Reference.Level2Collection).

    To include a collection and then a reference one level down: query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference)).

    To include a collection and then a collection one level down: query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection)).

    To include a collection and then a reference one level down: query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference)).

    To include a collection and then a collection one level down: query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection)).

    To include a collection, a reference, and a reference two levels down: query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference.Level3Reference)).

    To include a collection, a collection, and a reference two levels down: query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection.Select(l2 => l2.Level3Reference))).

This extension method calls the Include(String) method of the IQueryable source object, if such a method exists. If the source IQueryable does not have a matching method, then this method does nothing.

When you call the Include method, the query path is only valid on the returned instance of the IQueryable of T. Other instances of IQueryable of T and the context itself are not affected. You can call this method multiple times on an IQueryable of T to specify multiple paths for the query.