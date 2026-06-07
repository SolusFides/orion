using SmartDisplay.Shared.Models.Common;
using SmartDisplay.Shared.Models.Templates;

namespace SmartDisplay.ControlPanelClient.Services.Templates;

public interface ITemplatesService
{
    Task<List<TemplateDto>> GetTemplatesAsync();
    Task<TemplateDto?> GetTemplateByIdAsync(string id);
    Task<CreateTemplateResponse> CreateTemplateAsync(CreateTemplateRequest request);
    Task<ApiStatusResponse> UpdateTemplateAsync(string id, UpdateTemplateRequest request);
    Task<DeleteTemplateResponse> DeleteTemplateAsync(string id);
}
