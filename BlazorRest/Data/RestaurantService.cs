using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlazorRest.Data
{
    public class RestaurantService
    {
        public RestaurantContext DBContext { get; set; }

        public RestaurantService()
        {
            DBContext = new RestaurantContext();
        }

        public async Task<Product[]?> GetProductsAsync()
        {
            return await DBContext.Products.ToArrayAsync();
        }
        
        public async Task AddProduct()
        {
            DBContext.Products.Add(new Product());
            await DBContext.SaveChangesAsync();
        }

        public async Task<CrossProductUser[]?> GetCrossesAsync()
        {
            return await DBContext.CrossProductUsers.Include(s=>s.IdProductNavigation).ToArrayAsync();
        }

        public async Task UpdateProduct(int id, string title, string description)
        {
            var d = await DBContext.Products.FirstOrDefaultAsync(s => s.Id == id);
            if (d!=null)
            {
                d.Title = title;
                d.Description = description;
                DBContext.Entry(d).State = EntityState.Modified;
                await DBContext.SaveChangesAsync();
            }
        }

        public async Task RemoveProduct(int id)
        {
            var containItem = DBContext.Products.FirstOrDefault(s => s.Id == id);
            if (containItem != null)
            {
                var cross = await DBContext.CrossProductUsers.FirstOrDefaultAsync(s=>s.IdProduct == id);
                if (cross != null)
                {
                    DBContext.Remove(cross);
                }
                DBContext.Remove(containItem);
                await DBContext.SaveChangesAsync();
            }
        }

        public async Task RemoveFromCart(int id)
        {
            var containItem = DBContext.CrossProductUsers.FirstOrDefault(s => s.IdProduct == id);
            if (containItem != null)
            {
                DBContext.Remove(containItem);
                await DBContext.SaveChangesAsync();
            }

        }

        public async Task AddToCart(int id)
        {
            var containItem = DBContext.CrossProductUsers.FirstOrDefault(s => s.IdProduct == id);
            if (containItem != null)
            {
                containItem.CountProduct++;
            }
            else
            {
                await DBContext.CrossProductUsers.AddAsync(new CrossProductUser
                {
                    IdProduct = id,
                    IdUser = (await DBContext.Users.FirstOrDefaultAsync()).Id,
                    CountProduct = 1
                });
            }

            await DBContext.SaveChangesAsync();
        }
        public async Task UploadImage(byte[] data, int id)
        {
            var product = await DBContext.Products.FirstOrDefaultAsync(s => s.Id == id);
            if (product != null)
            {
                product.Icon = data;
                DBContext.SaveChanges();
            }
        }
    }
}