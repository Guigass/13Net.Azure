using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureSearch
{
    class IndexLetras
    {
        [Key]
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string Id { get; set; }

        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string NomeBanda { get; set; }

        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string Album { get; set; }

        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        [IsSearchable]
        public string Letra { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var searchServiceClient = new SearchServiceClient("teste-azuresearch-dois", 
                new SearchCredentials("4A91F9970AB73ADB4106D6BC56FD0EE2"));

            var index = searchServiceClient.Indexes.GetClient("index-bandas");
            var index2 = searchServiceClient.Indexes.Get("index-bandas");

            index2.Analyzers.Add(new CustomAnalyzer
            {
                Name = "custom",
                Tokenizer = TokenizerName.Standard,
                TokenFilters = new[]
                {
                    TokenFilterName.Phonetic,
                    TokenFilterName.Lowercase,
                    TokenFilterName.AsciiFolding
                }
            });

            index2.Fields[3].Analyzer = "custom";

            searchServiceClient.Indexes.CreateOrUpdate(index2, true);

            var batch = IndexBatch.Upload(new List<IndexLetras>
            {
                new IndexLetras
                {
                    Id = "rm330284",
                    Album = "Pelados em Santos",
                    NomeBanda = "Mamonas Assassinas",
                    #region Letra
                    Letra = @"Um tanto quanto másculo
                            Ai, com M maiúsculo
                            Vejam só os meus músculos
                            Que com amor cultivei
                            
                            Minha pistola é de plástico (quero chupar-pa-pa)
                            Em formato cilíndrico (quero chupar-pa)
                            Sempre me chamam de cínico (quero chupar)
                            Mas o porquê eu não sei (quero chupar-pa)
                            
                            O meu bumbum era flácido
                            Mas esse assunto é tão místico
                            Devido a um ato cirúrgico
                            Hoje eu me transformei
                            
                            O meu andar é erótico (silicone, yeah, yeah, yeah)
                            Com movimentos atômicos (silicone, yeah, yeah)
                            Sou um amante robótico (silicone, yeah)
                            Com direito a replay (silicone, yeah)
                            
                            Um ser humano fantástico
                            Com poderes titânicos
                            Foi um moreno simpático
                            Por quem me apaixonei
                            
                            E hoje estou tão eufórico (doce, doce, amor)
                            Com mil pedaços biônicos (doce, doce, amor)
                            Ontem eu era católico (doce, doce, amor)
                            Ai, hoje eu sou um gay!
                            
                            Abra sua mente
                            Gay também é gente
                            Baiano fala: Oxente
                            E come vatapá
                            
                            Você pode ser gótico
                            Ser punk ou skinhead
                            Tem gay que é Mohamed
                            Tentando camuflar
                            Alá, meu bom Alá
                            
                            Faça bem a barba
                            Arranque seu bigode
                            Gaúcho também pode
                            Não tem que disfarçar
                            
                            Faça uma plástica
                            Ai, entre na ginástica
                            Boneca cibernética
                            Um Robocop Gay
                            Um Robocop Gay
                            
                            Um Robocop Gay
                            Ai, eu sei, eu sei
                            Meu Robocop Gay
                            
                            Ai, como dói!"
	                #endregion
                }
            });

            //index.Documents.Index(batch);

            Console.WriteLine("Digite um termo para busca");
            var termo = Console.ReadLine();

            var parametres = new SearchParameters
            {
                IncludeTotalResultCount = true
            };
            var result = index.Documents.Search<IndexLetras>(termo, parametres);

            Console.WriteLine($"{result.Count} resultados encontrados");

            foreach (var item in result.Results)
                Console.WriteLine($"{item.Document.Id} - {item.Document.NomeBanda}");

            Console.Read();
        }
    }
}
