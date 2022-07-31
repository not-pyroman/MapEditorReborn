﻿// -----------------------------------------------------------------------
// <copyright file="MapEditorParentCommand.cs" company="MapEditorReborn">
// Copyright (c) MapEditorReborn. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace MapEditorReborn.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using ModifyingCommands;
    using ModifyingCommands.Position;
    using ModifyingCommands.Rotation;
    using ModifyingCommands.Scale;
    using ToolgunCommands;
    using UtilityCommands;

    /// <summary>
    /// The base parent command.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class MapEditorParentCommand : ParentCommand
    {
        /// <inheritdoc/>
        public MapEditorParentCommand() => LoadGeneratedCommands();

        /// <inheritdoc/>
        public override string Command => "mapeditor";

        /// <inheritdoc/>
        public override string[] Aliases { get; } = { "mp" };

        /// <inheritdoc/>
        public override string Description => "The MapEditorReborn parent command";

        /// <inheritdoc/>
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new CreateObject());
            RegisterCommand(new DeleteObject());
            RegisterCommand(new CopyObject());
            RegisterCommand(new SelectObject());

            RegisterCommand(new ToolGun());
            RegisterCommand(new GravityGun());
            RegisterCommand(new Save());
            RegisterCommand(new Load());
            RegisterCommand(new Unload());
            RegisterCommand(new ShowIndicators());
            RegisterCommand(new List());
            RegisterCommand(new OpenDirectory());
            RegisterCommand(new FixMaps());
            RegisterCommand(new Merge());

            RegisterCommand(new Properties());
            RegisterCommand(new Modify());
            RegisterCommand(new SetRoomType());
            RegisterCommand(new Position());
            RegisterCommand(new Rotation());
            RegisterCommand(new Scale());
        }

        /// <inheritdoc/>
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            response = "\nPlease enter a valid subcommand:\n\n";

            foreach (ICommand command in AllCommands)
            {
                if (player.CheckPermission($"mpr.{command.Command}"))
                {
                    response += $"<color=yellow><b>- {command.Command} ({string.Join(", ", command.Aliases)})</b></color>\n<color=white>{command.Description}</color>\n\n";
                }
            }

            return false;
        }
    }
}
