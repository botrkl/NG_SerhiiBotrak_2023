
namespace Skeleton.DAL.Entities
{
    public class Test: BaseEntity
    {
        public int Title { get; set; }
        public int Description { get; set; }

        public Guid? CreatedForUserId { get; set; }


        public virtual User? User { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
    }
}
