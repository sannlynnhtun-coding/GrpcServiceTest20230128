using Grpc.Core;
using GrpcServiceTest20230128.EFDbContext;
using GrpcServiceTest20230128.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceTest20230128.Services
{
    public class BlogService : Blog.BlogBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<BlogService> _logger;

        public BlogService(ILogger<BlogService> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public override async Task<BlogGrpcReply> GetBlogs(BlogPageGrpcRequest request, ServerCallContext context)
        {
            BlogGrpcReply reply = new BlogGrpcReply();
            try
            {
                var lst = await _db.Blogs.OrderByDescending(x => x.BlogId)
                    .ToListAsync();
                reply.Blog.AddRange
                    (
                        JsonConvert.DeserializeObject<IEnumerable<BlogItemGrpcReply>>
                        (JsonConvert.SerializeObject(lst))
                    );
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
            return reply;
        }

        //public override async Task<List<BlogModel>> GetBlogs(int pageNo = 1, int rowCount = 10)
        //{
        //    int skipRowCount = (pageNo - 1) * rowCount;
        //    var lst = await _db.Blogs.OrderByDescending(x => x.Blog_Id)
        //        .Skip(skipRowCount)
        //        .Take(rowCount)
        //        .ToListAsync();
        //    return lst;
        //}

        //public override async Task<BlogModel> GetBlog(int id)
        //{
        //    var item = await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
        //    return item;
        //}

        //public override async Task AddBlog(BlogModel blogModel)
        //{
        //    await _db.Blogs.AddAsync(blogModel);
        //    await _db.SaveChangesAsync();
        //}

        //public override async Task UpdateBlog(int id, BlogModel blogModel)
        //{
        //    var item = await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
        //    if (item == null) return;

        //    item.Blog_Id = id;
        //    item.Blog_Author = blogModel.Blog_Author;
        //    item.Blog_Title = blogModel.Blog_Title;
        //    item.Blog_Content = blogModel.Blog_Content;
        //    _db.Entry(item).State = EntityState.Modified;
        //    _db.Blogs.Update(item);
        //    await _db.SaveChangesAsync();
        //}

        //public override async Task DeleteBlog(int id)
        //{
        //    var item = await _db.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
        //    if (item == null) return;
        //    _db.Entry(item).State = EntityState.Deleted;
        //    _db.Blogs.Remove(item);
        //    await _db.SaveChangesAsync();
        //}
    }
}
