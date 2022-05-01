namespace SpellIt.Validation;

public class FolderValidator : AbstractValidator<Folder>
{
    public FolderValidator()
    {
        RuleFor<string>(f => f.Title).NotEmpty().MinimumLength(1).WithMessage("Please fill in title");
        RuleFor<string>(f => f.Author).NotEmpty().MaximumLength(1).WithMessage("Please fill in author");
        RuleFor<List<Set>>(f => f.Sets).NotEmpty().WithMessage("Please fill in a set");
    }
}
public class SetValidator : AbstractValidator<Set>
{
    public SetValidator()
    {
        RuleFor<string>(f => f.Title).NotEmpty().MinimumLength(1).WithMessage("Please fill in title");
        RuleFor<string>(f => f.Author).NotEmpty().MaximumLength(1).WithMessage("Please fill in author");
        RuleFor<string>(f => f.Language).NotEmpty().WithMessage("Please fill in a language");
        RuleFor<List<Word>>(f => f.Words).NotEmpty().WithMessage("Please fill in a word");
    }
}