using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Onion.Api.DTOs;
using Onion.Api.Errors;
using Onion.Api.Helper;
using Onion.Core;
using Onion.Core.Entities;
using Onion.Core.Repositories;
using Onion.Core.Specifications;

namespace Onion.Api.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IGenericRepository<ProductBrand> _bransRepository;
        public ProductsController(IGenericRepository<Product> productRepository,
            IMapper mapper,
            IGenericRepository<ProductType> TypeRepo,
            IGenericRepository<ProductBrand> BrandRepo)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _typeRepository = TypeRepo;
            _bransRepository = BrandRepo;
        }

        // Get all products
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery]ProductSpecParams Params)
        {
            var Spec = new ProductWithBrandAndTypeSpec(Params);

            var products = await _productRepository.GetAllWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>((IReadOnlyList<Product>)products);
            var CountSpec = new ProdctWithFilterationForCountAsync(Params);
            var Count = await _productRepository.GetCountWithSpecAsync(CountSpec);

            return Ok(new Pagination<ProductToReturnDTO>(Params.PageIndex, Params.PageSize, MappedProducts, Count));
        }

        // Get product by id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpec(id);
            var product = await _productRepository.GetByIdWithSpecAsync(Spec);
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }
        // Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes(IGenericRepository<ProductType> _typeRepository) 
        {
            var Types = await _typeRepository.GetAll();
            return Ok(Types);
        }

        // Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands(IGenericRepository<ProductBrand> _brandRepository)
        {
            var Brands = await _brandRepository.GetAll();
            return Ok(Brands);
        }
    }
}
