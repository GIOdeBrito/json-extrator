using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonExtrator
{
	internal class Start
	{
		static Start ()
		{
			CultureInfo customCulture = new CultureInfo("pt-BR");
			customCulture.NumberFormat.NumberDecimalSeparator = ".";

			// Define a "cultura" customizada a ser usada
			System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
		}

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

			string str_f = string.Empty;

			foreach(var item in itens)
			{
				Console.WriteLine();
				Console.WriteLine($"ID = #{item.id}");
				Console.WriteLine($"NOME = {item.nome}");

				double receita = double.Parse(item.receita);

				Console.WriteLine($"RECEITA = R${receita}");

				str_f += $"ID = #{item.id}";
				str_f += $" RECEITA = R${receita}";
				str_f += '\n';
			}

			File.WriteAllText("./saida.txt", str_f);
		}

		public static void ExtractKeyZero ()
		{
			string str = File.ReadAllText("./teste.json");

			OrderJson jf = JsonSerializer.Deserialize<OrderJson>(str);
			Dictionary<string,object> jd = JsonSerializer.Deserialize<Dictionary<string,object>>(jf.orders[0]);

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
		public string receita { get; set; }
	}
}
