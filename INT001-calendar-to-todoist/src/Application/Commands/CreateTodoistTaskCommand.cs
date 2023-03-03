namespace PD.INT001.Application.Commands;

public record CreateTodoistTaskCommand : IRequest;

internal sealed class CreateTodoistTaskHandler : IRequestHandler<CreateTodoistTaskCommand>
{
    public Task Handle(CreateTodoistTaskCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
