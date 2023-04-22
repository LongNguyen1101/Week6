using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Client.Abstractions;
using System.Collections;
using System.Net.Http;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;

namespace GRAPHQL
{
    public partial class GRAPHQLFORM : Form
    {
        private readonly GraphQLHttpClient _httpClient;

        public GRAPHQLFORM()
        {
            InitializeComponent();
            _httpClient = new GraphQLHttpClient("https://rickandmortyapi.com/graphql", new NewtonsoftJsonSerializer());
        }

        public class RickandMortyApi
        {
            public CharacterList characters { get; set; }
        }

        public class CharacterList
        {
            public info info { get; set; }
            public results[] results { get; set; }
        }
        public class info
        {
            public int count { get; set; }
        }
        public class results
        {
            public string name { get; set; }
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = @"
                    query {
                        characters (page: 2, filter: { name: ""rick"" }) {
                            info { count }
                            results { name }
                        }
                    }"
            };

            var graphQLResponse = await _httpClient.SendQueryAsync<RickandMortyApi>(graphQLRequest);

            if (graphQLResponse.Errors != null)
            {
                MessageBox.Show("Error for receive API!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var data = graphQLResponse.Data;
                var results = data.characters.results;
                dgvShow.Rows.Clear();

                foreach (var result in results)
                {
                    var name = result.name.ToString();
                    dgvShow.Rows.Add(name);
                }
            }

        }
    }
}
