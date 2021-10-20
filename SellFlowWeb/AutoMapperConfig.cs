using Models;
using AutoMapper;
using SellFlowWeb.Models.DataView;
using SellFlowWeb.Models.ApiRequest;

namespace SellFlowWeb
{
    public class AutoMapperConfig : Profile
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<PessoaDataView, PessoaModel>().ReverseMap();
                cfg.CreateMap<PessoaApiRequest, PessoaModel>().ReverseMap();

                cfg.CreateMap<UsuarioDataView, UsuarioModel>().ReverseMap();
                cfg.CreateMap<UsuarioApiRequest, UsuarioModel>().ReverseMap();

                cfg.CreateMap<CategoriaDataView, CategoriaModel>().ReverseMap();
                cfg.CreateMap<CategoriaApiRequest, CategoriaModel>().ReverseMap();

                cfg.CreateMap<ProdutoDataView, ProdutoModel>().ReverseMap();
                cfg.CreateMap<ProdutoApiRequest, ProdutoModel>().ReverseMap();

                //cfg.CreateMap<PermissaoDataView, PermissaoModel>().ReverseMap();
                //cfg.CreateMap<PermissaoApiRequest, PermissaoModel>().ReverseMap();

                //cfg.CreateMap<PermissaoPaginaDataview, PermissaoPaginaModel>().ReverseMap();
                //cfg.CreateMap<PermissaoPaginaApiRequest, PermissaoPaginaModel>().ReverseMap();

                //cfg.CreateMap<PaginaDataView, PaginaModel>().ReverseMap();
                //cfg.CreateMap<PaginaApiRequest, PaginaModel>().ReverseMap();

                //cfg.CreateMap<AnuncioDataView, AnuncioModel>().ReverseMap();
                //cfg.CreateMap<AnuncioApiRequest, AnuncioModel>().ReverseMap();

                //cfg.CreateMap<ImagensDataView, ImagensModel>().ReverseMap();
                //cfg.CreateMap<ImagensApiRequest, ImagensModel>().ReverseMap();

                //cfg.CreateMap<AnuncioSituacaoDataView, AnuncioSituacaoModel>().ReverseMap();
                //cfg.CreateMap<AnuncioSituacaoApiRequest, AnuncioSituacaoModel>().ReverseMap();
            });

            return config;
        }

    }
}
