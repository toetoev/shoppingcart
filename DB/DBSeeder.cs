using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.DB;
using Microsoft.VisualBasic.CompilerServices;

namespace ShoppingCart.DB
{
    public class DBSeeder
    {
        public DBSeeder(CartContext dbcontext)
        {
            dbcontext.Customers.Add(new Customer("admin", "123"));

            dbcontext.Customers.Add(new Customer("chris", "123"));

            dbcontext.Products.Add(new Product(1,"MineCraft", "Minecraft is a 3D sandbox game that has no specific goals to accomplish, allowing players a large amount of freedom.", 22.90, "images/minecraft.jpg"));

            dbcontext.Products.Add(new Product(2,"Sims 4", "Sims 4 is a life simulation game that gives you the power to create and control people, and experience the creativity, humor.", 59.90, "images/sims4.jpg"));

            dbcontext.Products.Add(new Product(3,"Grand Theft Auto V", "Grand Theft Auto V is an action-adventure game where you can roam the open world as much as you wish and complete the story.", 20.90, "images/gtac.jpg"));

            dbcontext.Products.Add(new Product(4,"DOOM Eternal", "DOOM Eternal is a first-person shooter game developed by ID Software and published by Bethesda Softworks.", 57.90, "images/doom.jpg"));

            dbcontext.Products.Add(new Product(5,"Overwatch", "Overwatch is a 2016 first-person shooter multiplayer video game for PC, developed and published by Blizzard Entertainment.", 20.90, "images/overwatch.jpg"));

            dbcontext.Products.Add(new Product(6,"Sid Meier's Civilization V", "Sid Meier's Civilization V is the fifth installment of a popular turn-based strategy game in choosing how to play the game.", 21.90, "images/civilization.jpg"));

            dbcontext.Products.Add(new Product(7,"FIFA 20", "FIFA 20 is another installment in the long line of the football simulation games based on the FIFA license around the American city of Boston.", 37.90, "images/fifa20.jpg"));

            dbcontext.Products.Add(new Product(8,"The Elder Scrolls V", "The Elder Scrolls V: Skyrim is a perfect open world game. RPG games allow you to take on a role you want and to create a character.", 24.90, "images/skyrim.jpg"));

            dbcontext.Products.Add(new Product(9,"FallOut 4", "Fallout 4 is a post-apocalyptic RPG game where the player is exploring the wasteland around the American city of Boston. ", 14.90, "images/fallout4.jpg"));

            dbcontext.Products.Add(new Product(10,"Call of Duty Black Ops II", "Call of Duty: Black Ops II is a first - person shooter(FPS) developed by Treyarch and published by Activision.", 10.90, "images/call.jpg"));

            dbcontext.Products.Add(new Product(11,"Human: Fall Flat", "Human: Fall Flat is a physics-based puzzle game where you control both arms separately of a blob-like character.", 15.90, "images/human.jpg"));

            dbcontext.Products.Add(new Product(12,"Stellaris", "Stellaris is a 4X grand strategy video game developed and published by Paradox Interactive. Stellaris's gameplay revolves around space exploration.", 29.90, "images/stellaris.jpg"));

            dbcontext.SaveChanges();
        }
    }
}
