using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Storage;
using VDS.RDF.Query;


namespace ConsoleApp2
{
    class Program
    {
        public static String getGrandfather(String grandchild )  // sparql query to get the grandfather
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://localhost:8890/sparql"), "http://localhost:8890/test");
            ////Make a SELECT query against the Endpoint
            SparqlResultSet results = endpoint.QueryWithResultSet("SELECT ?grandParent WHERE { <http://bedrock/"+grandchild+"> <http://bedrock/hasParent> ?o.  ?o <http://bedrock/hasParent> ?grandParent}");
            String res = "";
            foreach (SparqlResult result in results)
            { 
                res += result.ToString().Split('=')[result.ToString().Split('=').Length -1].Split('/')[result.ToString().Split('=')[result.ToString().Split('=').Length - 1].Split('/').Length-1];
            }
            return res;
        }

        public static IGraph addTrippleToFHKB(IGraph FHKB , String subject, String predicate, String objec)
        {
            INode sub = FHKB.CreateUriNode(UriFactory.Create("http://bedrock/"+subject));
            INode pred = FHKB.CreateUriNode(UriFactory.Create("http://bedrock/"+predicate));
            INode obj = FHKB.CreateUriNode(UriFactory.Create("http://bedrock/"+objec));
            Triple t = new Triple(sub, pred, obj);
            FHKB.Assert(t);
            //foreach (Triple k in g.Triples)
            //{
            //    Console.WriteLine(k.ToString());
            //}
            return FHKB;
        }
        public static void Main(string[] args)
        {
            //VirtuosoManager manager = new VirtuosoManager("localhost", VirtuosoManager.DefaultPort, VirtuosoManager.DefaultDB, "dba", "dba");
            //IGraph g = new Graph();
            //manager.LoadGraph(g,new Uri("http://localhost:8890/test"));
            //INode ali = g.CreateUriNode(UriFactory.Create("http://bedrock/Ali"));
            //INode hasParent = g.CreateUriNode(UriFactory.Create("http://bedrock/hasParent"));
            //INode mullar = g.CreateUriNode(UriFactory.Create("http://bedrock/Mullar"));
            //Triple t = new Triple(ali, hasParent, mullar);
            ////g.Assert(t);
            //foreach (Triple k in g.Triples)
            //{
            //    Console.WriteLine(k.ToString());
            //}
            //Console.WriteLine(g.BaseUri);
            ////manager.SaveGraph(g);          // to save updates happen to the graph
            ////Console.WriteLine(g.IsEmpty);
            //Console.ReadLine();
            while (true)
            {
                Console.WriteLine("Please enter The name of the grandchild: ");
                String grandchildName = Console.ReadLine();
                Console.WriteLine(getGrandfather(grandchildName));
            }

        }
    }
}
