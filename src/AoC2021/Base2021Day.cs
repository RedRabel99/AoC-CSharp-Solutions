﻿using Common;

namespace AoC_2021;

public abstract class Base2021Day : BaseLibraryDay
{
    protected override int Year => 2021;

    /// <summary>
    /// Additional customization
    /// </summary>
    protected override string ClassPrefix => base.ClassPrefix + Year;
}