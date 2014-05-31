using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klod.Web.Semantic.Common;
using Klod.Web.Semantic.Interfaces;
using Klod.Web.Semantic;

namespace SimpleTripleStore
{
	class Program
	{
	   //static void Main(string[] args)
	   //{
	   //    #region SimpleGraph definition and test (chapter 2 pp. 23-26)
	   //    SimpleGraph sg = new SimpleGraph();
		 
	   //    #region Add triples with add function (p.24)
		  
	   //    //add triples
	   //    //sg.Add("claudio rivera", "padre", "laura isabel rivera");
	   //    //sg.Add("maría andújar", "madre", "laura isabel rivera");
	   //    //sg.Add("laura isabel rivera", "hija", "claudio rivera");
	   //    //sg.Add("maría andújar", "escribe", "cuentos");
	   //    //sg.Add("laura isabel rivera", "es", "niña");
	   //    //sg.Add("claudio rivera", "es", "programador");
	   //    //sg.Add("claudio rivera", "sabe", "C#");
	   //    //sg.Add("claudio rivera", "sabe", "XML");
	   //    //sg.Add("claudio rivera", "sabe", "VB");
	   //    //sg.Save(@"Z:\\my_triples2.csv");
	   //    #endregion

	   //    #region Query store using Triples function (pp. 25-26)
	   //    //Console.WriteLine(String.Join("\n", sg.Triples("claudio rivera", "", "")));
	   //    //Console.WriteLine(String.Join("\n", sg.Triples("", "hija", "")));
	   //    //Console.WriteLine(String.Join("\n", sg.Triples("", "", "claudio rivera")));
	   //    //Console.WriteLine(String.Join("\n", sg.Triples("maría andújar", "", "laura isabel rivera")));
	   //    //Console.WriteLine(String.Join("\n", sg.Triples(null, null, "programador")));
	   //    //Console.WriteLine("\n");
	   //    #endregion

	   //    #region Remove values of stored triples with remove method, test value() function (p. 24)
	   //    //sg.Remove("maría andújar", "madre", "laura isabel rivera");
	   //    //sg.Remove("laura isabel rivera", "hija", null);
	   //    //sg.Remove("claudio rivera", "", "VB");
	   //    //sg.Remove("maría andújar", "", "");
	   //    //sg.Remove(null, "sabe", "XML");
	   //    //sg.Remove(null, null, "C#");
	   //    //sg.Remove(null, "sabe", null);
	   //    //Console.WriteLine(String.Join("\n", sg.Triples(null, null, null)));
	   //    //Console.WriteLine("\n");
	   //    #endregion

	   //    #region Query a single value (p. 26)
	   //    //Console.WriteLine(sg.Value("claudio rivera", "padre", null));
	   //    //Console.WriteLine(sg.Value("", "madre", "laura isabel rivera"));
	   //    //Console.WriteLine(sg.Value("claudio rivera", "", "programador"));
	   //    //Console.WriteLine(sg.Value("claudio rivera", "sabe", null));
	   //    //Console.WriteLine("\n");

	   //    #endregion


	   //    #region Merge multiple loaded triple stores (pp. 24-28)

	   //    //a. load triples files and merge
	   //    SimpleGraph sg1 = new SimpleGraph();
	   //    SimpleGraph sg2 = new SimpleGraph();
	   //    SimpleGraph sg3 = new SimpleGraph();
	   //    SimpleGraph sg4 = new SimpleGraph();

	   //    //load method (p. 24)
	   //    sg1.Load(@"Z:\\sw\business_triples.csv");
	   //    sg2.Load(@"z:\\sw\\celeb_triples.csv");
	   //    sg3.Load(@"Z:\\sw\\movies.csv");
	   //    sg4.Load(@"z:\\sw\\place_triples.txt");

	   //    SimpleGraph merge = new SimpleGraph();
	   //    string[] t1 = sg1.Triples(null, null, null);
	   //    string[] t2 = sg2.Triples(null, null, null);
	   //    string[] t3 = sg3.Triples(null, null, null);
	   //    string[] t4 = sg4.Triples(null, null, null);

	   //    //merge all triples (pp. 26-28)
	   //    foreach (string t in t1)
	   //    {
	   //        if (t == string.Empty)
	   //            continue;
	   //        string[] v = t.Split(',');
	   //        merge.Add(v[0], v[1], v[2]);
	   //    }
	   //    foreach (string t in t2)
	   //    {
	   //        if (t == string.Empty)
	   //            continue;
	   //        string[] v = t.Split(',');
	   //        merge.Add(v[0], v[1], v[2]);
	   //    }

	   //    foreach (string t in t3)
	   //    {
	   //        if (t == string.Empty)
	   //            continue;
	   //        string[] v = t.Split(',');
	   //        merge.Add(v[0], v[1], v[2]);
	   //    }

	   //    foreach (string t in t4)
	   //    {
	   //        if (t == string.Empty)
	   //            continue;
	   //        string[] v = t.Split(',');
	   //        merge.Add(v[0], v[1], v[2]);
	   //    }
	   //    # endregion

	   //    #region test content of the full merged triple store
	   //    //Console.WriteLine(String.Join("\n", merge.Triples("AMMD", null, null)));
	   //    //Console.WriteLine(String.Join("\n", merge.Triples("/en/blade_runner", null, null)));
	   //    //Console.WriteLine(String.Join("\n", merge.Triples(null, "name", "Southern California")));
	   //    //Console.WriteLine(String.Join("\n", merge.Triples(null, "directed_by", "/en/ridley_scott")));
	   //    //Console.WriteLine("\n");
	   //    #endregion           
	   //    #region test complex queries (get a value id and get objects based on values)

	   //    //string brID = merge.Value(null, "name", "Blade Runner");//get id of movie blade runner
		 
	   //    //Console.WriteLine(brID);
	   //    //Console.WriteLine();

	   //    //string[] starsTriples = merge.Triples(brID, "starring", null); //get all actors id from blade runner
	   //    //foreach (string a in starsTriples)
	   //    //{
	   //    //    string[] obj = a.Split(',');
	   //    //    Console.WriteLine(obj[2]);
	   //    //    Console.WriteLine(merge.Value(obj[2], "name", null));
	   //    //    Console.WriteLine();
	   //    //}
	   //    #endregion
	   //    #region Get mayors of california
	   //    //string[] tr = sg4.Triples(null, "inside", "California");

	   //    //if (tr == null)
	   //    //    return;

	   //    //foreach (string t in tr)
	   //    //{
	   //    //    string[] c = t.Split(',');
	   //    //    tr = sg4.Triples(c[0], "mayor", "");
	   //    //    if (tr != null)
	   //    //    {
	   //    //        Console.Write(String.Join("\n", sg4.Triples(c[0], "mayor", "")));
	   //    //        Console.WriteLine(t);
	   //    //    }
	   //    //}
	   //    #endregion
	   //    #region Query method
	   ///////////////////////////////////////////////////////////////////////////////////////
	   //////1. Get company, money and quantity of contributions to Orrin Hatch
	   ///////////////////////////////////////////////////////////////////////////////////////
	   ////string[] results = merge.Query("?company,headquarters,New_York_New_York"
	   ////                    , "?company,industry,\"Investment Banking\""
	   ////                    , "?contribution,contributor,?company"
	   ////                    , "?contribution,recipient,Orrin Hatch"
	   ////                    , "?contribution,amount,?dollars");

	   ////if (results != null)
	   ////{
	   ////    foreach (string r in results)
	   ////    {
	   ////        Console.WriteLine(r);
	   ////    }
	   ////}
	   ////Console.WriteLine();
	   //    //Console.WriteLine();

	   //    ///////////////////////////////////////////////////////////////////////////////////
	   //    //2. Get persons that had relations with Britney Spears boyfriends
	   //    ////////////////////////////////////////////////////////////////////////////
	   //    string[] results2 = merge.Query("?relation1,with,?person1"
	   //                       , "?relation1,with,Britney Spears"
	   //                       , "?relation1,end,?year1"
	   //                       , "?relation2,with,?person1"
	   //                       , "?relation2,start,?year1");

	   //    if (results2 != null)
	   //    {
	   //        foreach (string r in results2)
	   //        {
	   //            Console.WriteLine(r);
	   //        }
	   //    }

	   //    Console.WriteLine();
	   //    //3. Get movies and actors who star movies by ridley scott and george lucas
	   //    //Console.WriteLine();
	   //    //string[] results3 = merge.Query("?movie1,directed_by,/en/ridley_scott"
	   //    //                   , "?movie1,starring,?actor"
	   //    //                   , "?movie2,directed_by,/en/george_lucas"
	   //    //                   , "?movie2,starring,?actor");

	   //    //if (results3 != null)
	   //    //{
	   //    //    foreach (string r in results3)
	   //    //    {
	   //    //        Console.WriteLine(r);
	   //    //    }
	   //    //}
	   //    #endregion
	   //    #region inference rule 
	   //    ///*
	   //    // *   WestCoastRule
	   //    //*/

	   //    //WestCoastRule westRule = new WestCoastRule();
	   //    //merge.ApplyInference(westRule);

	   //    //string[] testRule = merge.Triples(null, "on_coast", null); //get all companies from coast
	   //    //foreach (string a in testRule)
	   //    //{
	   //    //    Console.WriteLine(a);
	   //    //}

	   //    //Console.WriteLine();
	   //    /*
	   //     * EnemyRule
	   //     */
	   //    //First test the actual enemies.
	   //    //string[] testRule2 = merge.Triples(null, "enemy", null); //get all enemies
	   //    //if (testRule2 == null)
	   //    //    return;
	   //    //foreach (string a in testRule2)
	   //    //{
	   //    //    Console.WriteLine(a);
	   //    //}
	   //    //Console.WriteLine();

	   //    ////Apply enemy rule   
	   //    //EnemyRule enemyRule = new EnemyRule();
	   //    //merge.ApplyInference(enemyRule);
	   //    ////check triples again
	   //    //testRule2 = merge.Triples(null, "enemy", null);
	   //    //foreach (string a in testRule2)
	   //    //{
	   //    //    Console.WriteLine(a);
	   //    //}
	   //    #endregion
	   //    #region query multiple triple stores
	   //    //1. Get the IT companies, city and region
	   //    //string[] results3 = merge.Query("?company,headquarters,?city"
	   //    //                   , "?city,inside,?region"
	   //    //                   , "?company,industry,Computer software");

	   //    //if (results3 != null)
	   //    //{
	   //    //    foreach (string r in results3)
	   //    //    {
	   //    //        Console.WriteLine(r);
	   //    //    }
	   //    //}
	   //    //Console.WriteLine();
	   //    ////2. Get the population of investment banks
	   //    //results3 = merge.Query("?company,headquarters,?city"
	   //    //                   , "?city,population,?pop"
	   //    //                   , "?company,industry,Investment banking");

	   //    //if (results3 != null)
	   //    //{
	   //    //    foreach (string r in results3)
	   //    //    {
	   //    //        //split each binding
	   //    //        string[] b = r.Split(',');
	   //    //        string[] p = b[2].Split(':');
	   //    //        if(Int32.Parse(p[1])>1000000)
	   //    //            Console.WriteLine(r);
	   //    //    }
	   //    //}

	   //    #endregion
	   //    #region Using GraphViz
	   //    //merge.SaveTriplesToDotFile(@"Z:\\sw\MyFirstTriplesGraph.gv",merge.Triples(null,"with",null));
	   //    merge.SaveQueryToDotFile(@"Z:\\sw\MyFirstQueryGraph.gv", merge.Query("?rel,with,?p1", "?rel,with,?p2"), "?p1", "?p2");
	   //    #endregion
	   //    Console.Read();
	   //    #endregion
	   //}

	   static void Main(string[] args)
	   {
		  //2013.11.28 crr tested
		  #region 1. Create a triple store class and add values
		  ITripleStore store = RootFactory.GetInstance().MakeTripleStore() ;

		  //store.Add("Claudio Rivera", "padre", "Laura Isabel Rivera");
		  //store.Add("Laura Isabel Rivera", "hija", "María Isabel Andújar");
		  //store.Add("Claudio Rivera", "esposo", "María Isabel Andújar");


		  #endregion

		  //2013.11.28 crr tested
		  #region 2. Save triple store in memory
		  //store.Save("z:\\myNewTripleStoreFile.csv");
		  #endregion

		  ////2013.11.28 crr tested
		  #region 3. Load triple store from file
		  //store.Load(@"Z:\\sw\\business_triples.csv");
		  store.Load(@"Z:\sw\celeb_triples.csv");

		  #endregion
		  ////2013.11.28 crr tested
		  #region 4. Get all triples
		  //ITriple[] triples = store.GetTriples();

		  //foreach (ITriple triple in triples)
		  //{
		  //    Console.WriteLine(triple.Serialize(','));
		  //}
		  #endregion
		  ////2013.11.28 crr tested
		  #region 5. Get triples by criteria
		  //ITriple[] triples = store.GetTriples("Claudio Rivera", "", "");

		  //foreach (ITriple triple in triples)
		  //{
		  //    Console.WriteLine(triple.Serialize(','));
		  //}
		  //Console.WriteLine();
		  //triples = store.GetTriples("", "hija", "");
		  //foreach (ITriple triple in triples)
		  //{
		  //    Console.WriteLine(triple.Serialize(','));
		  //}
		  //Console.WriteLine();
		  //triples = store.GetTriples("", "", "Laura Isabel Rivera");
		  //foreach (ITriple triple in triples)
		  //{
		  //    Console.WriteLine(triple.Serialize(','));
		  //}
		  //Console.WriteLine();
		  //triples = store.GetTriples("Claudio Rivera", "esposo", "");
		  //foreach (ITriple triple in triples)
		  //{
		  //    Console.WriteLine(triple.Serialize(','));
		  //}
		  #endregion

		  //2013.11.28 crr tested
		  #region 6. Remove triple
		  //Triple triple = new Triple("Claudio Rivera","","");
		  //store.Remove(triple);
		  //Console.Write(store.SerializeTripleListInMemory());
		  #endregion

		  #region 7. Get Parameter bindings by query
		  IQuerySet querySet = RootFactory.GetInstance().MakeQuerySet();
		  ////Company, contribution and dollars
		  //IQuery query = querySet.MakeQuery("?company","headquarters","New_York_New_York");
		  //querySet.Add(query);
		  //query = querySet.MakeQuery("?company","industry","Investment Banking");
		  //querySet.Add(query);
		  //query = querySet.MakeQuery("?contribution","contributor","?company");
		  //querySet.Add(query);
		  //query = querySet.MakeQuery("?contribution","recipient","Orrin Hatch");
		  //querySet.Add(query);
		  //query = querySet.MakeQuery("?contribution","amount","?dollars");
		  //querySet.Add(query);
		  ////relations
		  IQuery query = querySet.MakeQuery("?rel1", "with", "?person");
		  querySet.Add(query);
		  query = querySet.MakeQuery("?rel1", "with", "Cameron Diaz");
		  querySet.Add(query);
		  query = querySet.MakeQuery("?rel1", "end", "?year1");
		  querySet.Add(query);
		  query = querySet.MakeQuery("?rel2", "with", "?person");
		  querySet.Add(query);
		  query = querySet.MakeQuery("?rel2", "start", "?year1");
		  querySet.Add(query);

		  IParameterBindingSet[] paramsBindings = store.GetParameterBindings2(querySet.Queries);
		  foreach (IParameterBindingSet pb in paramsBindings)
		  {
			 Console.WriteLine(pb.Serialize(':',','));
		  }

		  //IParameterBindingSet paramsBindings = querySet.GetParameterBindings();

		  //foreach (IParameterBinding pb in paramsBindings.Bindings)
		  //{
		  //    Console.WriteLine(pb.Serialize(':'));
		  //}

		  //string[] results =store.GetParameterBindings(("?company,headquarters,New_York_New_York"
		  //                    , "?company,industry,\"Investment Banking\""
		  //                    , "?contribution,contributor,?company"
		  //                    , "?contribution,recipient,Orrin Hatch"
		  //                    , "?contribution,amount,?dollars");

		  #endregion
		  Console.Read();

	   }
	}
}