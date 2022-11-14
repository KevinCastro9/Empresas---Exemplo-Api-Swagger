using Models.Empresas;
using Models.Grupo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views.Empresas;
using Views.Grupo;

namespace Repository
{
    public class PrincipalRepository
    {
        private string caminhoProjeto = @"C:\Users\kevin\Desktop\PROJETOS\C#\Projeto Exemplo - Api Swagger\";

        private string arquivoGroup = @"ApiDesafioDotNet\BibliotecaArquivos\group.json";
        private string arquivoCompany = @"ApiDesafioDotNet\BibliotecaArquivos\company.json";
        private List<Grupo> LerArquivoGrupo()
        {
            try
            {
                StreamReader r = new StreamReader(caminhoProjeto + arquivoGroup);
                string jsonString = r.ReadToEnd();
                List<Grupo> grupo = JsonConvert.DeserializeObject<List<Grupo>>(jsonString);
                r.Close();

                return grupo;
            }
            catch
            {
                return null;
            }

        }

        private bool EscreverArquivoGrupo(List<Grupo> grupos)
        {
            try
            {
                using (StreamWriter file = File.CreateText(caminhoProjeto + arquivoGroup))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, grupos);
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        private List<Empresas> LerArquivoEmpresas()
        {
            try
            {
                StreamReader r = new StreamReader(caminhoProjeto + arquivoCompany);
                string jsonString = r.ReadToEnd();
                List<Empresas> empresas = JsonConvert.DeserializeObject<List<Empresas>>(jsonString);
                r.Close();

                return empresas;
            }
            catch
            {
                return null;
            }

        }

        private bool EscreverArquivoEmpresas(List<Empresas> empresas)
        {
            try
            {
                using (StreamWriter file = File.CreateText(caminhoProjeto + arquivoCompany))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, empresas);
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        public Empresas SelectEmpresasId(string id)
        {
            try
            {
                List<Empresas> empresas = LerArquivoEmpresas();

                for (int i = 0; i < empresas.Count; i++)
                {
                    if (empresas[i].Id == id)
                    {
                        return empresas[i];
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Grupo SelectGrupoId(int id)
        {
            try
            {
                List<Grupo> grupos = LerArquivoGrupo();

                for (int i = 0; i < grupos.Count; i++)
                {
                    if (grupos[i].Id == id)
                    {
                        return grupos[i];
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<Empresas> SelectEmpresasDate(DateTime date)
        {
            try
            {
                List<Empresas> lerEmpresas = LerArquivoEmpresas();

                List<Empresas> empresas = new List<Empresas>();

                for (int i = 0; i < lerEmpresas.Count; i++)
                {
                    if (lerEmpresas[i].Date_Ingestion <= date)
                    {
                        empresas.Add(lerEmpresas[i]);
                    }
                }

                return empresas;
            }
            catch
            {
                return null;
            }
        }

        public bool InsertGrupo(GrupoView grupoView)
        {
            try
            {
                List<Grupo> grupos = LerArquivoGrupo();

                for (int i = 0; i < grupos.Count; i++)
                {
                    if (grupos[i].Id == grupoView.Id)
                    {
                        return false;
                    }
                }

                Grupo grupo = new Grupo(grupoView.Id, grupoView.Nome, grupoView.Category, grupoView.date_ingestion);

                grupos.Add(grupo);

                return EscreverArquivoGrupo(grupos); ;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertEmpresa(EmpresaView empresaView)
        {
            try
            {
                List<Empresas> empresas = LerArquivoEmpresas();

                for (int i = 0; i < empresas.Count; i++)
                {
                    if (empresas[i].Id == empresaView.Id)
                    {
                        return false;
                    }
                }

                Empresas empresa = new Empresas(empresaView.Id, empresaView.Status, empresaView.Date_Ingestion);

                empresas.Add(empresa);

                return EscreverArquivoEmpresas(empresas);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateEmpresaCustos(string id, Custos recebeCustos)
        {
            try
            {
                List<Empresas> empresas = LerArquivoEmpresas();

                for (int i = 0; i < empresas.Count; i++)
                {
                    if (empresas[i].Id == id)
                    {
                        if (empresas[i].Custos != null)
                        {
                            for (int j = 0; j < empresas[i].Custos.Count; j++)
                            {
                                if (empresas[i].Custos[j].Id_Type == recebeCustos.Id_Type && empresas[i].Custos[j].Ano == recebeCustos.Ano)
                                {
                                    empresas[i].Custos[j].Valor = recebeCustos.Valor;
                                    return EscreverArquivoEmpresas(empresas);
                                }
                            }
                            empresas[i].Custos.Add(recebeCustos);
                            return EscreverArquivoEmpresas(empresas);
                        }
                        else
                        {
                            empresas[i].Custos = new List<Custos> { recebeCustos };
                            return EscreverArquivoEmpresas(empresas);
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateGrupoEmpresas(int idGrupo, string idEmpresa)
        {
            try
            {
                List<Grupo> grupos = LerArquivoGrupo();
                List<Empresas> empresas = LerArquivoEmpresas();

                for (int i = 0; i < empresas.Count; i++)
                {
                    if (empresas[i].Id == idEmpresa)
                    {
                        for (int j = 0; j < grupos.Count; j++)
                        {
                            if (grupos[j].Id == idGrupo)
                            {
                                if (grupos[j].Companys != null)
                                {
                                    grupos[j].Companys.Add(empresas[i].Id);
                                    return EscreverArquivoGrupo(grupos);
                                }
                                else
                                {
                                    grupos[j].Companys = new List<string> { empresas[i].Id };
                                    return EscreverArquivoGrupo(grupos);
                                }
                            }
                        }
                        return false;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEmpresa(string id)
        {
            try
            {
                List<Empresas> empresas = LerArquivoEmpresas();

                for (int i = 0; i < empresas.Count; i++)
                {
                    if (empresas[i].Id == id)
                    {
                        empresas[i].Status = "INATIVO";
                        return EscreverArquivoEmpresas(empresas);
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public TiposCustosView SelectGrupoCustos(int id)
        {
            try
            {
                TiposCustosView tiposCustosView = new TiposCustosView();
                tiposCustosView.Valor = new List<double>();

                List<Grupo> grupos = LerArquivoGrupo();
                List<Empresas> empresas = LerArquivoEmpresas();

                for (int i = 0; i < grupos.Count; i++)
                {
                    if (grupos[i].Id == id)
                    {
                        if (grupos[i].Companys != null)
                        {
                            for (int j = 0; j < grupos[i].Companys.Count; j++)
                            {
                                Empresas leituraEmpresas = SelectEmpresasId(grupos[i].Companys[j]);

                                for (int a = 0; a < leituraEmpresas.Custos.Count; a++)
                                {
                                    for (int b = 0; b < tiposCustosView.Id_Type.Count; b++)
                                    {
                                        if (tiposCustosView.Id_Type[b] == leituraEmpresas.Custos[a].Id_Type)
                                        {
                                            tiposCustosView.Valor[b] += leituraEmpresas.Custos[a].Valor;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                return tiposCustosView;
            }
            catch
            {
                return null;
            }
        }
    }
}
