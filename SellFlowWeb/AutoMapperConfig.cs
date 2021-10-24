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
                cfg.CreateMap<PessoaApiRequest, PessoaDataView>()
                    .ForMember(dest => dest.usuarioObj, opt => opt.MapFrom(src => new UsuarioDataView()))
                    .AfterMap((src, dest) => dest.usuarioObj.id =src.id)
                    .ReverseMap();

                cfg.CreateMap<UsuarioDataView, UsuarioModel>().ReverseMap();
                cfg.CreateMap<UsuarioApiRequest, UsuarioModel>().ReverseMap();
                cfg.CreateMap<UsuarioApiRequest, UsuarioDataView>()
                    .ReverseMap();

                cfg.CreateMap<CategoriaDataView, CategoriaModel>().ReverseMap();
                cfg.CreateMap<CategoriaApiRequest, CategoriaModel>().ReverseMap();
                cfg.CreateMap<CategoriaApiRequest, CategoriaDataView>().ReverseMap();

                cfg.CreateMap<ProdutoDataView, ProdutoModel>().ReverseMap();

                cfg.CreateMap<ProdutoApiRequest, ProdutoModel>().ReverseMap();
                cfg.CreateMap<ProdutoApiRequest, ProdutoDataView>().ReverseMap()
                    .ForMember(dest => dest.usuario, opt => opt.MapFrom(src => src.usuariovendedorObj.id))
                    .ForMember(dest => dest.categoria, opt => opt.MapFrom(src => src.categoriaObj.id))
                    .ReverseMap();

                cfg.CreateMap<AnuncioDataView, AnuncioModel>().ReverseMap();
                cfg.CreateMap<AnuncioApiRequest, AnuncioModel>().ReverseMap();
                cfg.CreateMap<AnuncioApiRequest, AnuncioDataView>()
                    //.ForMember(dest => dest.produtoObj, opt => opt.MapFrom(src => new ProdutoDataView()))
                    //.AfterMap((src,dest) => dest.produtoObj.id = src.produto)
                    //.ForMember(dest => dest.anuncioSituacaoObj, opt => opt.MapFrom(src => new AnuncioSituacaoDataView()))
                    //.AfterMap((src, dest) => dest.anuncioSituacaoObj.id = src.anunciosituacao)
                    .ReverseMap()
                    .ForMember(dest => dest.produto, opt => opt.MapFrom(src => src.produtoObj.id))
                    .ForMember(dest => dest.anunciosituacao, opt => opt.MapFrom(src => src.anuncioSituacaoObj.id));

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

                cfg.CreateMap<AnuncioSituacaoDataView, AnuncioSituacaoModel>().ReverseMap();
                //cfg.CreateMap<AnuncioSituacaoApiRequest, AnuncioSituacaoModel>().ReverseMap();
            });

            return config;
        }

    }
}
