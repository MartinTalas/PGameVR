using System.Collections;
using System.Collections.Generic;

public class Achievement
{
    //---[ Constructors ]---
    public Achievement() { }
    public Achievement(int id_arg, string title_arg, string description_arg)
    {
        this.ID = id_arg;
        this.Title = title_arg;
        this.Description = description_arg;
    }

    //---[ Private variables ]---
    private int id;
    private string title;
    private string description;
    //image?

    //---[ Getters - Setters]---
    public int ID { get { return this.id; } set { this.id = value; } }
    public string Title { get { return this.title; } set { this.title = value; } }
    public string Description { get { return this.description; } set { this.description = value; } }
}
