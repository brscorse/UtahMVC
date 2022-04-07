using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UtahMVC.Models.ViewModels;

namespace UtahMVC.Infrastructure
{

    [HtmlTargetElement("div", Attributes = "page-blah")]
    public class PaginationTagHelper : TagHelper
    {

        // dynamically create page links
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PageBlah { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public bool PageClassesEnabled { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        // function to loop to build links to different pages

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");


            // check to see if total pages is less than 11 and can fit on the bar
            if (PageBlah.TotalPages < 12)
            {
                for (int i = 1; i <= PageBlah.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageBlah.CurrentPage ? PageClassSelected : PageClassNormal);
                    }

                    tb.InnerHtml.Append(i.ToString());
                    final.InnerHtml.AppendHtml(tb);
                }
            }

            // check to see if the number of pages left is less than 10 and then show 10 and the end 
            else if (PageBlah.CurrentPage + 10 > PageBlah.TotalPages)
            {
                for (int i = PageBlah.TotalPages - 10; i <= PageBlah.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageBlah.CurrentPage ? PageClassSelected : PageClassNormal);
                    }

                    tb.InnerHtml.Append(i.ToString());
                    final.InnerHtml.AppendHtml(tb);
                }
            }

            // add ten pages else statement
            else
            {
                for (int i = PageBlah.CurrentPage; i <= PageBlah.CurrentPage + 10; i++)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageBlah.CurrentPage ? PageClassSelected : PageClassNormal);
                    }

                    tb.InnerHtml.Append(i.ToString());
                    final.InnerHtml.AppendHtml(tb);
                }
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
