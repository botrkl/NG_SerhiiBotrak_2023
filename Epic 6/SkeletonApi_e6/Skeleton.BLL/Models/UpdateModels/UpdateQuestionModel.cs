namespace Skeleton.BLL.Models.AddModels;

public class UpdateQuestionModel
{
    public string Id { get; set; }
    public string Text { get; set; }

    public List<AddAnswerModel> Answers { get; set; }
}