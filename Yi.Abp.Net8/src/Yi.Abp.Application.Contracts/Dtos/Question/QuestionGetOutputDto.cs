
using Volo.Abp.Application.Dtos;

namespace Yi.Abp.Application.Contracts.Dtos.Question
{
    public class QuestionGetOutputDto : EntityDto<Guid>
    {
        public string? project { get; set; }
        public string? status { get; set; }
        public string? category { get; set; }
        public string? impact { get; set; }
        public string? priority { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? solve_user { get; set; }
        public string? solve_description { get; set; }
        public DateTime CreationTime { get; set; }
        public string Create_name { get; set; }

    }
}
