﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roadkill.Core;
using Roadkill.Core.Configuration;
using Roadkill.Core.Database;
using Roadkill.Core.Services;
using Roadkill.Core.Mvc.ViewModels;
using Roadkill.Core.Plugins;

namespace Roadkill.Tests.Unit
{
	public class SearchServiceMock : SearchService
	{
		public List<Page> Pages { get; set; }
		public List<PageContent> PageContents { get; set; }

		public SearchServiceMock(ApplicationSettings settings, IRepository repository, IPluginFactory pluginFactory)
			: base(settings, repository, pluginFactory)
		{
			Pages = new List<Page>();
			PageContents = new List<PageContent>();
		}

		public override void Add(PageSummary summary)
		{	
		}

		public override void Update(PageSummary summary)
		{	
		}

		public override void CreateIndex()
		{
		}

		public override int Delete(PageSummary summary)
		{
			return 1;
		}

		public override IEnumerable<SearchResult> Search(string searchText)
		{
			List<SearchResult> results = new List<SearchResult>();

			foreach (Page page in Pages.Where(p => p.Title.ToLowerInvariant().Contains(searchText.ToLowerInvariant())))
			{
				results.Add(new SearchResult()
				{
					Id = page.Id,
					Title = page.Title,
					ContentSummary = PageContents.Single(p => p.Page == page).Text
				});
			}

			foreach (PageContent pageContent in PageContents.Where(p => p.Text.ToLowerInvariant().Contains(searchText.ToLowerInvariant())))
			{
				results.Add(new SearchResult()
				{
					Id = pageContent.Page.Id,
					ContentSummary = pageContent.Text,
					Title = pageContent.Page.Title
				});
			}

			return results;
		}
	}
}