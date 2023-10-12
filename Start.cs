using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonExtrator
{
	internal class Start
	{
		public Start() { }

		public static void Extract ()
		{
			string str = File.ReadAllText("./teste.json");

			var jorders = JsonSerializer.Deserialize<OrderJson>(str);

			List<CupomItemTeste> itens = new List<CupomItemTeste>();

			foreach(var kvp in jorders.orders)
			{
				//Console.WriteLine(kvp.GetType());
				var jitem = JsonSerializer.Deserialize<Dictionary<string, object>>(kvp);

				foreach(var jkey in jitem)
				{
					Console.WriteLine("KEY = " + jkey.Key);
					//Console.WriteLine("VALUE = " + jkey.Value);

					itens.Add(JsonSerializer.Deserialize<CupomItemTeste>(jkey.Value));
				}
			}

			Console.WriteLine();

			foreach(var item in itens)
			{
				Console.WriteLine();
				Console.WriteLine($"ID = #{item.id}");
				Console.WriteLine($"NOME = {item.nome}");
				Console.WriteLine($"RECEITA = R${item.receita}");
			}
		}

		public static void ExtractKeyZero ()
		{
			string str = File.ReadAllText("./teste.json");

			var jf = JsonSerializer.Deserialize<OrderJson>(str);

			var jd = JsonSerializer.Deserialize<Dictionary<string, object>>(jf.orders[0]);

			foreach(var kvp in jd)
			{
				Console.WriteLine(kvp.Key);
			}
		}
	}

	public class OrderJson
	{
		public OrderJson() { }

		public List<dynamic> orders { get; set; }
	}

	public class CupomItemTeste
	{
		public int id { get; set; }
		public string nome { get; set; }
		public int receita { get; set; }
	}
}
