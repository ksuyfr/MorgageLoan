using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MorgageLoan.Data;
using MorgageLoan.Dtos.Credit;
using MorgageLoan.Interfaces;
using MorgageLoan.Mappers;

namespace MorgageLoan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICreditRepository _creditRepo;
        public CreditController(ApplicationDBContext context, ICreditRepository creditRepo)
        {
            _context = context;
            _creditRepo = creditRepo; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var credits = await _creditRepo.GetAllAsync();
            
            var creditDto= credits.Select(s => s.ToCreditDto());
            return Ok(credits);
        }
#if (false)
        [HttpGet("{id:int}, {ttf:bool}")]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromRoute] bool ttf)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var credit = await _creditRepo.GetByIdAsync(id);

            if (credit == null)
            {
                return NotFound();
            }

            if (ttf)
            {
                TestFirstFeeDto testFirstFeeDto = new TestFirstFeeDto();
                testFirstFeeDto.Id = id;
                testFirstFeeDto.FirstPercent = (double?)Math.Round(Decimal.Divide(credit.FirstFloor, credit.FullCoast) * 100, 2,
                    MidpointRounding.ToZero);
                testFirstFeeDto.FirstFloor = (decimal)((double)credit.FullCoast * (credit.FirstPercent ?? 0) / 100);
                return Ok(testFirstFeeDto);
            }
            else
                return Ok(credit.ToCreditDto());
        }
#else
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var credit = await _creditRepo.GetByIdAsync(id);

            if (credit == null)
            {
                return NotFound();
            }
            return Ok(credit.ToCreditDto());
        }
#endif
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCreditRequestDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creditModel = createDto.ToCreditFromCreateDto();
            await _creditRepo.CreateAsync(creditModel);
            return CreatedAtAction(nameof(GetById), new { id = creditModel.Id }, creditModel.ToCreditDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCreditRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creditModel = await _creditRepo.UpdateAsync(id, updateDto);
            if (creditModel == null)
            {
                return NotFound();
            }

            return Ok(creditModel.ToCreditDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creditModel = await _creditRepo.DeleteAsync(id);
            if (creditModel == null)
                return NotFound();
            return NoContent();
        }
#if (false)
        [HttpGet("calculate/{id_tff:int}")]
        public async Task<IActionResult> TestFirstFeeById([FromRoute] int id_tff)
        {
            //По ID извлекаю сохраненные данные кредита и по ним пересчитываю соответствие 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var credit = await _creditRepo.GetByIdAsync(id_tff);

            if (credit == null)
            {
                return NotFound();
            }
            TestFirstFeeDto testFirstFeeDto = new TestFirstFeeDto();
            testFirstFeeDto.Id = id_tff;
            testFirstFeeDto.FirstPercent = (double?)Math.Round(Decimal.Divide(credit.FirstFloor, credit.FullCoast) * 100, 2,
                MidpointRounding.ToZero);
            testFirstFeeDto.FirstFloor = (decimal)((double)credit.FullCoast * (credit.FirstPercent ?? 0) / 100);

            return Ok(testFirstFeeDto);
        }
#else
        [HttpGet("TestFirstFee")]
        public IActionResult TestFirstFee([FromQuery] TestFirstFeeDto checkInput)
        {
            TestFirstFeeDto testFirstFeeDto = new TestFirstFeeDto();
            testFirstFeeDto.FullCoast = checkInput.FullCoast;
            testFirstFeeDto.FirstPercent = (double)Math.Round(Decimal.Divide(checkInput.FirstFloor, checkInput.FullCoast) * 100, 2,
                MidpointRounding.ToZero);
            testFirstFeeDto.FirstFloor = (decimal)((double)checkInput.FullCoast * (checkInput.FirstPercent) / 100);

            return Ok(testFirstFeeDto);
        }
#endif

    }
 }
