using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.TextureAtlases;  // TextureRegion
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.Brushes;

namespace BeeGame;

public static class BuildUI
{
    public static void CreateBasicBuildUI(Desktop desktop)
    {
        // Main container for bottom-left box
        var panel = new Panel
        {
            Width = 180*2,
            Height = 120*2,
            Background = new SolidBrush(Color.Gray)
        };

        // Position bottom-left
        panel.HorizontalAlignment = HorizontalAlignment.Left;
        panel.VerticalAlignment = VerticalAlignment.Bottom;

        // Grid for 4 buttons
        var grid = new Grid()
        {
            ColumnSpacing = 6 * 2,
            RowSpacing = 6 * 2
        };

        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));

        Button MakeTexButton(Texture2D texture)
        {
            var image = new Image
            {
                Renderable = new TextureRegion(texture),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                ResizeMode = ImageResizeMode.Stretch
            };

            var container = new Panel
            {
                Width = 57 * 2,
                Height = 57 * 2
            };

            container.Widgets.Add(image);

            return new Button
            {
                Padding = new Thickness(0),
                BorderThickness = new Thickness(0),
                Width = 57 * 2,
                Height = 57 * 2,
                Content = container
            };
        }

        var bee_comb_button = MakeTexButton(Textures.BeeComb1);
        grid.Widgets.Add(bee_comb_button);
        
        var pool_button = MakeTexButton(Textures.GooPool);
        Grid.SetColumn(pool_button, 1);
        grid.Widgets.Add(pool_button);
        
        var button3 = MakeTexButton(Textures.white_circle);
        Grid.SetRow(button3, 1);
        grid.Widgets.Add(button3);
        
        var button4 = MakeTexButton(Textures.white_circle);
        Grid.SetColumn(button4, 1);
        Grid.SetRow(button4, 1);
        grid.Widgets.Add(button4);
        
        var button5 = MakeTexButton(Textures.white_circle);
        Grid.SetColumn(button5, 2);
        grid.Widgets.Add(button5);
        
        var button6 = MakeTexButton(Textures.white_circle);
        Grid.SetColumn(button6, 2);
        Grid.SetRow(button6, 1);
        grid.Widgets.Add(button6);

        // button handlers
        bee_comb_button.Click += (s, a) =>
        {
            BuildingToolsLogic.ClickBuildingMenuHandler(BuildingType.HoneyComb);
        };
        
        pool_button.Click += (s, a) =>
        {
            BuildingToolsLogic.ClickBuildingMenuHandler(BuildingType.Pool);
        };

        panel.Widgets.Add(grid);
        desktop.Widgets.Add(panel);
    }
}