using News.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace News.Interface
{
    public interface IViewService
    {
        List<FakeNews> GetSixNews();

        int Create(FakeNewsViewModel model, string path);

        List<Category> GetCategory();

        SelectList GetCategorySelect();

        SelectList GetCategorySelect(int? id);

        void CreateCat(Category categories);

        FakeNews ViewNews(int? id);

        void Edit(FakeNewsViewModel fakeNews, string path);
 
        void Delete(FakeNews fakeNews);

        List<FakeNews> CategoryViews(int? id);

        FakeNewsViewModel ViewNewsEdit(int? id);
    }
}
