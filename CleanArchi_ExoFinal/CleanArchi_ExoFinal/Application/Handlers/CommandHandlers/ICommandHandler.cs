﻿using CleanArchi_ExoFinal.Application.Kernel;

namespace CleanArchi_ExoFinal.Kernel;

public interface ICommandHandler<R, C> where C : Message
{
    public R Handle(C message);
}
