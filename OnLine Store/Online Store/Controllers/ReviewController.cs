using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;

        public ReviewController(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _reviewRepo.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var review = await _reviewRepo.GetReviewByIdAsync(id);

            if(review == null)
            {
                return NotFound();
            }

            return Ok(review);  
        }

        [HttpPost]
        public async Task<IActionResult> PostReview(Review review)
        {
            await _reviewRepo.AddReviewAsync(review);
            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if(id != review.Id)
            {
                return BadRequest();
            }

            try
            {
                await _reviewRepo.UpdateReviewAsync(review);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _reviewRepo.GetReviewByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewRepo.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await _reviewRepo.DeleteReviewAsync(id);
            return NoContent();
        }

        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            var reviews = await _reviewRepo.GetReviewsByProductIdAsync(productId);
            return Ok(reviews);
        }
    }
}
