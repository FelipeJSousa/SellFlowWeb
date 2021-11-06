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

                cfg.CreateMap<EnderecoDataView, EnderecoModel>().ReverseMap();
                cfg.CreateMap<EnderecoApiRequest, EnderecoModel>().ReverseMap();
                cfg.CreateMap<EnderecoApiRequest, EnderecoDataView>().ReverseMap();

                cfg.CreateMap<UsuarioDataView, UsuarioModel>().ReverseMap();
                cfg.CreateMap<UsuarioApiRequest, UsuarioModel>().ReverseMap();
                cfg.CreateMap<UsuarioApiRequest, UsuarioDataView>().ReverseMap();

                cfg.CreateMap<CategoriaDataView, CategoriaModel>().ReverseMap();
                cfg.CreateMap<CategoriaApiRequest, CategoriaModel>().ReverseMap();
                cfg.CreateMap<CategoriaApiRequest, CategoriaDataView>().ReverseMap();

                cfg.CreateMap<ProdutoApiRequest, ProdutoModel>().ReverseMap();
                cfg.CreateMap<ProdutoDisplayDataView, ProdutoModel>()
                    .ForMember(dest => dest.valor, opt => opt.MapFrom(src => double.Parse(src.valorDecimal)))
                    .ReverseMap()
                    .ForMember(dest => dest.valorDecimal, opt => opt.MapFrom(src => src.valor.ToString("N2")));
                cfg.CreateMap<ProdutoDataView, ProdutoModel>()
                    .ForMember(dest => dest.valor, opt => opt.MapFrom(src => double.Parse(src.valorDecimal)))
                    .ReverseMap()
                    .ForMember(dest => dest.valorDecimal, opt => opt.MapFrom(src => src.valor.ToString("N2")));
                cfg.CreateMap<ProdutoApiRequest, ProdutoDisplayDataView>().ReverseMap()
                    .ForMember(dest => dest.usuario, opt => opt.MapFrom(src => src.usuariovendedorObj.id))
                    .ForMember(dest => dest.categoria, opt => opt.MapFrom(src => src.categoriaObj.id))
                    .ForMember(dest => dest.valor, opt => opt.MapFrom(src => double.Parse(src.valorDecimal)))
                    .ReverseMap()
                    .ForMember(dest => dest.valorDecimal, opt => opt.MapFrom(src => src.valor.ToString("N2")));
                cfg.CreateMap<ProdutoApiRequest, ProdutoDataView>().ReverseMap()
                    .ForMember(dest => dest.usuario, opt => opt.MapFrom(src => src.usuariovendedorObj.id))
                    .ForMember(dest => dest.categoria, opt => opt.MapFrom(src => src.categoriaObj.id))
                    .ForMember(dest => dest.valor, opt => opt.MapFrom(src => double.Parse(src.valorDecimal)))
                    .ReverseMap()
                    .ForMember(dest => dest.valorDecimal, opt => opt.MapFrom(src => src.valor.ToString("N2")));

                cfg.CreateMap<AnuncioDataView, AnuncioModel>()
                    .ForMember(dest => dest.valor, opt => opt.MapFrom(src => double.Parse(src.valorDecimal)))
                    .ReverseMap()
                    .ForMember(dest => dest.valorDecimal, opt => opt.MapFrom(src => src.valor.ToString("N2")));
                cfg.CreateMap<AnuncioApiRequest, AnuncioModel>().ReverseMap();
                cfg.CreateMap<AnuncioApiRequest, AnuncioDataView>()
                    .ForMember(dest => dest.valorDecimal, opt => opt.MapFrom(src => src.valor.ToString("N2")))
                    .ReverseMap()
                    .ForMember(dest => dest.valor, opt => opt.MapFrom(src => double.Parse(src.valorDecimal)))
                    .ForMember(dest => dest.produto, opt => opt.MapFrom(src => src.produtoObj.id))
                    .ForMember(dest => dest.anunciosituacao, opt => opt.MapFrom(src => src.anuncioSituacaoObj.id));


                cfg.CreateMap<AnuncioSituacaoDataView, AnuncioSituacaoModel>().ReverseMap();

                //cfg.CreateMap<PermissaoDataView, PermissaoModel>().ReverseMap();
                //cfg.CreateMap<PermissaoApiRequest, PermissaoModel>().ReverseMap();

                //cfg.CreateMap<PermissaoPaginaDataview, PermissaoPaginaModel>().ReverseMap();
                //cfg.CreateMap<PermissaoPaginaApiRequest, PermissaoPaginaModel>().ReverseMap();

                //cfg.CreateMap<PaginaDataView, PaginaModel>().ReverseMap();
                //cfg.CreateMap<PaginaApiRequest, PaginaModel>().ReverseMap();


                //cfg.CreateMap<ImagensDataView, ImagensModel>().ReverseMap();
                //cfg.CreateMap<ImagensApiRequest, ImagensModel>().ReverseMap();
            });

            return config;
        }

    }
}
