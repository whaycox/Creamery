using System;
using System.Collections.Generic;
using System.Text;
using Queso.Domain;

namespace Queso.Application.Character
{
    public interface ICharacter
    {
        Domain.Character Load(string filePath);
    }
}
