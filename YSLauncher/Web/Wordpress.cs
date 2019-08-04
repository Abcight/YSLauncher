using System;

public class Rootobject
{
    public int found { get; set; }
    public Post[] posts { get; set; }
    public Meta meta { get; set; }
}

public class Meta
{
    public Links links { get; set; }
    public string next_page { get; set; }
    public bool wpcom { get; set; }
}

public class Links
{
    public string counts { get; set; }
}

public class Post
{
    public int ID { get; set; }
    public int site_ID { get; set; }
    public Author author { get; set; }
    public DateTime date { get; set; }
    public DateTime modified { get; set; }
    public string title { get; set; }
    public string URL { get; set; }
    public string short_URL { get; set; }
    public string content { get; set; }
    public string excerpt { get; set; }
    public string slug { get; set; }
    public string guid { get; set; }
    public string status { get; set; }
    public bool sticky { get; set; }
    public string password { get; set; }
    public bool parent { get; set; }
    public string type { get; set; }
    public Discussion discussion { get; set; }
    public bool likes_enabled { get; set; }
    public bool sharing_enabled { get; set; }
    public int like_count { get; set; }
    public bool i_like { get; set; }
    public bool is_reblogged { get; set; }
    public bool is_following { get; set; }
    public string global_ID { get; set; }
    public string featured_image { get; set; }
    public object post_thumbnail { get; set; }
    public string format { get; set; }
    public bool geo { get; set; }
    public int menu_order { get; set; }
    public string page_template { get; set; }
    public object[] publicize_URLs { get; set; }
    public Terms terms { get; set; }
    public Tags tags { get; set; }
    public Categories categories { get; set; }
    public Attachments attachments { get; set; }
    public int attachment_count { get; set; }
    public bool metadata { get; set; }
    public Meta3 meta { get; set; }
    public Capabilities capabilities { get; set; }
    public Other_Urls other_URLs { get; set; }
}

public class Author
{
    public int ID { get; set; }
    public string login { get; set; }
    public bool email { get; set; }
    public string name { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string nice_name { get; set; }
    public string URL { get; set; }
    public string avatar_URL { get; set; }
    public string profile_URL { get; set; }
    public int site_ID { get; set; }
}

public class Discussion
{
    public bool comments_open { get; set; }
    public string comment_status { get; set; }
    public bool pings_open { get; set; }
    public string ping_status { get; set; }
    public int comment_count { get; set; }
}

public class Terms
{
    public Category category { get; set; }
    public Post_Tag post_tag { get; set; }
    public Post_Format post_format { get; set; }
    public Mentions mentions { get; set; }
}

public class Category
{
    public Uncategorized Uncategorized { get; set; }
}

public class Uncategorized
{
    public int ID { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public string description { get; set; }
    public int post_count { get; set; }
    public int parent { get; set; }
    public Meta1 meta { get; set; }
}

public class Meta1
{
    public Links1 links { get; set; }
}

public class Links1
{
    public string self { get; set; }
    public string help { get; set; }
    public string site { get; set; }
}

public class Post_Tag
{
}

public class Post_Format
{
}

public class Mentions
{
}

public class Tags
{
}

public class Categories
{
    public Uncategorized1 Uncategorized { get; set; }
}

public class Uncategorized1
{
    public int ID { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public string description { get; set; }
    public int post_count { get; set; }
    public int parent { get; set; }
    public Meta2 meta { get; set; }
}

public class Meta2
{
    public Links2 links { get; set; }
}

public class Links2
{
    public string self { get; set; }
    public string help { get; set; }
    public string site { get; set; }
}

public class Attachments
{
}

public class Meta3
{
    public Links3 links { get; set; }
}

public class Links3
{
    public string self { get; set; }
    public string help { get; set; }
    public string site { get; set; }
    public string replies { get; set; }
    public string likes { get; set; }
}

public class Capabilities
{
    public bool publish_post { get; set; }
    public bool delete_post { get; set; }
    public bool edit_post { get; set; }
}

public class Other_Urls
{
}
