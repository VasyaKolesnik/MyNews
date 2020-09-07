using News.Interface;
using News.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace News.Service
{
    public class ViewService: IViewService
    {
        NewsContext db = new NewsContext();

        public List<FakeNews> GetSixNews()
        {
            List<FakeNews> news = db.FakeNews.Include(p => p.Category).OrderByDescending(p => p.Date).Take(6).ToList();
            return news;
        }

        public int Create(FakeNewsViewModel model, string path) 
        {
            if (model.ImgFile != null)
            {
                string fileName = System.IO.Path.GetFileName(model.ImgFile.FileName);
                model.ImgFile.SaveAs(path + fileName);
                model.Image = fileName;
            }
            var fakeNews = new FakeNews()
            {
                Name = model.Name,
                Text = model.Text,
                Date = DateTime.Now,
                CategoryId = model.CategoryId,
                Image = model.Image
            };
            
            db.FakeNews.Add(fakeNews);
            db.SaveChanges();
            return fakeNews.Id;
        }
        public void CreateCat(Category categories)
        {
            db.Categories.Add(categories);
            db.SaveChanges();
        }

        public FakeNews ViewNews(int? id)
        {
            return db.FakeNews.Find(id);
        }        
        public FakeNewsViewModel ViewNewsEdit(int? id)
        {
            var fakeNews = db.FakeNews.Find(id);
            FakeNewsViewModel value = new FakeNewsViewModel {
            Id = fakeNews.Id,
            CategoryId = fakeNews.CategoryId,
            Name = fakeNews.Name,
            Text = fakeNews.Text};
            return value;
        }

        public void Edit(FakeNewsViewModel model, string path)
        {
            if (model.ImgFile != null)
            {
                string fileName = System.IO.Path.GetFileName(model.ImgFile.FileName);
                model.ImgFile.SaveAs(path + fileName);
                model.Image = fileName;
            }

            var fakeNews = db.FakeNews.Find(model.Id);
            if (model.Image != null) fakeNews.Image = model.Image;
            if (model.Text != null && model.Text != fakeNews.Text) fakeNews.Text = model.Text;
            if (model.Name != null && model.Name != fakeNews.Name) fakeNews.Name = model.Name;
            if (model.CategoryId != 0 && model.CategoryId != fakeNews.CategoryId) fakeNews.CategoryId = model.CategoryId;
            fakeNews.Date = DateTime.Now;
            db.Entry(fakeNews).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(FakeNews fakeNews)
        {
            db.FakeNews.Remove(fakeNews);
            db.SaveChanges();
        }

        public List<FakeNews> CategoryViews(int? id)
        {
            return db.FakeNews.Include(p => p.Category).Where(p => p.CategoryId == id).ToList();
        }

        public List<Category> GetCategory()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public SelectList GetCategorySelect()
        {
            SelectList categories = new SelectList(db.Categories, "Id", "CategoryNews");
            return categories;
        }
        public SelectList GetCategorySelect(int? id)
        {
            SelectList categories = new SelectList(db.Categories, "Id", "CategoryNews", id);
            return categories;
        }
    }
}