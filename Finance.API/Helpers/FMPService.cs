using AutoMapper;
using Finance.API.Interfaces;
using Finance.API.Models;
using Newtonsoft.Json;

namespace Finance.API.Helpers
{
	public class FMPService : IFMPService
	{
		private HttpClient _httpClient;
		private IConfiguration _configuration;
		private readonly IMapper _mapper;

		public FMPService(HttpClient httpClient, IConfiguration configuration, IMapper mapper)
		{
			_httpClient = httpClient;
			_configuration = configuration;
			_mapper = mapper;
		}

		public async Task<Stock> FindStockBySymbolAsync(string symbol)
		{
			try
			{
				var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["FMPKey"]}");
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
					var fMPStock = tasks[0];
					if (fMPStock != null)
					{
						var maperStock = _mapper.Map<Stock>(fMPStock);
						return maperStock;
					}
					return null;
				}	
				return null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
	}
}
