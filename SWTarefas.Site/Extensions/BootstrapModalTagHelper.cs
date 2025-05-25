using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SWTarefas.Site.Extensions
{
    [HtmlTargetElement("bootstrap-modal")]
    public class BootstrapModalTagHelper : TagHelper
    {
        private readonly IAntiforgery _antiforgery;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string Id { get; set; } = "meuModal";
        public string Title { get; set; } = "Título do Modal";
        public string UrlAjax { get; set; } = "controller/action";

        public BootstrapModalTagHelper(IAntiforgery antiforgery, IHttpContextAccessor httpContextAccessor)
        {
            _antiforgery = antiforgery;
            _httpContextAccessor = httpContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var token = "";
            if (_httpContextAccessor.HttpContext != null)
                token = _antiforgery.GetAndStoreTokens(_httpContextAccessor.HttpContext).RequestToken;

            output.TagName = null;

            string modalHtml = $@"
                <div id=""{Id}"" class=""modal"" tabindex=""-1"">
	                <div class=""modal-dialog"">
		                <div class=""modal-content"">
			                <div class=""modal-header"">
				                <h5 class=""modal-title"">{Title}</h5>
				                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close"" onclick=""FecharModalExclusao()""></button>
			                </div>
			                <div class=""modal-body"">
				                {output.GetChildContentAsync().Result.GetContent()}
			                </div>
			                <div class=""modal-footer"">
				                <button id=""{Id}BtnSimExcluir"" type=""button"" class=""btn btn-success btn-sm""><i class=""fa-solid fa-check"" style=""margin-right: 5px;""></i>Sim</button>
				                <button type=""button"" class=""btn btn-secondary btn-sm"" data-bs-dismiss=""modal"" onclick=""FecharModalExclusao()""><i class=""fa-solid fa-xmark"" style=""margin-right: 5px;""></i>Não</button>
			                </div>
		                </div>
	                </div>
                </div>
				<script>
					function ExcluirConfirm(id){{
						var modalExclusao = $('#{Id}');
						modalExclusao.show();

						var BtnSimExcluir = $('#{Id}BtnSimExcluir');

						BtnSimExcluir.on('click', function()
						{{
							$.ajax({{
								url: ""{UrlAjax}"",
								data: {{__RequestVerificationToken: '{token}', id: id}},
								method: 'POST',
								success: function(){{					
									FecharModalExclusao();

									location.reload(true);
								}}			
							}});
						}});
					}}
					function FecharModalExclusao(){{
						var modalExclusao = $('#{Id}');
						modalExclusao.hide();
					}}
				</script>";

            output.Content.SetHtmlContent(modalHtml);
        }
    }
}
