namespace TrainingPlanner.Core.DTOs.Trainer
{
    public class TrainerUpdateDTO : TrainerCreateDTO
    {
        public int Id { get; set; }
        public int ViewCounter { get; set; }
    }
}
