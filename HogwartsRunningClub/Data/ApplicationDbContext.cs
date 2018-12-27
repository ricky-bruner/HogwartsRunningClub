using System;
using System.Collections.Generic;
using System.Text;
using HogwartsRunningClub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HogwartsRunningClub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<House> House { get; set; }
        public DbSet<Race> Race { get; set; }
        public DbSet<TopicCategory> TopicCategory { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Workout> Workout { get; set; }
        public DbSet<UserRace> UserRace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.DateRegistered)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Topic>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Comment>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Topic>()
                .HasMany(t => t.Comments)
                .WithOne(c => c.Topic)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TopicCategory>()
                .HasMany(tc => tc.Topics)
                .WithOne(t => t.TopicCategory)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserTopics)
                .WithOne(t => t.User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserComments)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserWorkouts)
                .WithOne(w => w.User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserRaces)
                .WithOne(ur => ur.User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Race>()
                .HasMany(r => r.RaceParticipants)
                .WithOne(rp => rp.Race)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<House>()
                .HasMany(h => h.HouseUsers)
                .WithOne(hu => hu.House)
                .OnDelete(DeleteBehavior.Restrict);

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "AdminaStraytor",
                Location = "Nashville, TN",
                MilesRun = 134.8,
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<House>().HasData(
                new House()
                {
                    HouseId = 1,
                    Title = "Gryffindor",
                    Description = "Gryffindor House is named after Hogwarts Founder Godric Gryffindor and is home to young witchs and wizards that model exemplary courage, bravery, and nerve!",
                },
                new House()
                {
                    HouseId = 2,
                    Title = "Hufflepuff",
                    Description = "Hufflepuff House is named after Hogwarts Founder Helga Hufflepuff and is home to young witchs and wizards that model honesty, loyalty, justice, and patience!",
                },
                new House()
                {
                    HouseId = 3,
                    Title = "Ravenclaw",
                    Description = "Ravenclaw House is named after Hogwarts Founder Rowena Ravenclaw and is home to young witchs and wizards that model exemplary intelligence, creativity, wit, and learning!",
                },
                new House()
                {
                    HouseId = 4,
                    Title = "Slytherin",
                    Description = "Slytherin House is named after Hogwarts Founder Salazar Slytherin and is home to young witchs and wizards that model ambition, cunning, shrewdness, and self-preservation!",
                }
            );

            modelBuilder.Entity<TopicCategory>().HasData(
                new TopicCategory()
                { 
                    TopicCategoryId = 1,
                    Label = "General"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 2,
                    Label = "Lore"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 3,
                    Label = "Charity"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 4,
                    Label = "Races"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 5,
                    Label = "Fitness"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 6,
                    Label = "Personal"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 7,
                    Label = "Humor"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 8,
                    Label = "Books"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 9,
                    Label = "Movies"
                },
                new TopicCategory()
                {
                    TopicCategoryId = 10,
                    Label = "Rumors"
                }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race() 
                { 
                    RaceId = 1,
                    Title = "Sorcerer's Stone 5k",
                    CharityName = "The Dana-Farber Cancer Institute / Jimmy Fund",
                    CharityDescription = "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 2,
                    Title = "Chamber of Secrets 10k",
                    CharityName = "The Dana-Farber Cancer Institute / Jimmy Fund",
                    CharityDescription = "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.",
                    Distance = 6.2,
                },
                new Race()
                {
                    RaceId = 3,
                    Title = "A Walk In the Moonlight 5k",
                    CharityName = "The Dana-Farber Cancer Institute / Jimmy Fund",
                    CharityDescription = "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 4,
                    Title = "Hedwig Memorial Run 5k",
                    CharityName = "The Dana-Farber Cancer Institute / Jimmy Fund",
                    CharityDescription = "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 5,
                    Title = "Platform 9 3/4k Year One",
                    CharityName = "The Dana-Farber Cancer Institute / Jimmy Fund",
                    CharityDescription = "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.",
                    Distance = 6.06,
                },
                new Race()
                {
                    RaceId = 6,
                    Title = "Triwizard Challenge 5k/10k/15k",
                    CharityName = "The Dana-Farber Cancer Institute / Jimmy Fund",
                    CharityDescription = "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.",
                    Distance = 18.64,
                },
                new Race()
                {
                    RaceId = 7,
                    Title = "Deathly Hallows Challenge",
                    CharityName = "The Dana-Farber Cancer Institute / Jimmy Fund",
                    CharityDescription = "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 8,
                    Title = "Dementor's Kiss 5k",
                    CharityName = "Miles for Cystic Fibrosis",
                    CharityDescription = "An Atlanta-based running organization that raises awareness of CF, sponsors CF research, and directly supports local families dealing with this terrible disease. Their youth organization, Rosebuds, is a youth running club founded by a brave little girl named Elana with the theme of “Kids helping kids with CF.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 9,
                    Title = "Weasley’s Wizarding Wheezer 5k",
                    CharityName = "Dogs on Deployment",
                    CharityDescription = "An awesome charity that connects military members with loving foster homes to care for their pets when they have to deploy overseas. They also provide financial assistance for emergency medical services for military pets.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 10,
                    Title = "Voldemort V-Miler",
                    CharityName = "Achilles International",
                    CharityDescription = "An incredible organization with one simple mission: To enable people with all types of disabilities to participate in mainstream athletics in order to promote personal achievement, enhance self esteem, and lower barriers to living a fulfilling life. Whether it’s supplying racing wheelchairs to athletes or connecting a guide with a vision-impaired runner, Achilles International ensures that an athlete’s disability doesn't limit their potential.",
                    Distance = 5.0,
                },
                new Race()
                {
                    RaceId = 11,
                    Title = "Dept. of Mysteries 6.2442k",
                    CharityName = "Brotherhood Ride",
                    CharityDescription = "A Florida-based group of firefighters, police officers and EMTs who ride bicycles to honor those who have died in the line of duty. These volunteers ride hundreds of miles on or near the anniversary of the honoree’s ultimate sacrifice to remind the families, friends, and the community of the fallen that they are not forgotten. The result of their rides, and this event, were donations made directly to the families of those who gave their lives to protect the rest of us.",
                    Distance = 3.88,
                },
                new Race()
                {
                    RaceId = 12,
                    Title = "Platform 9 3/4k Year Two",
                    CharityName = "Harry Potter Alliance Accio Books! Campaign",
                    CharityDescription = "Accio Books! is the Harry Potter Alliance’s annual book drive and since 2009, Harry Potter fans around the world have donated more than 350,000 books to underprivileged or under-served readers.",
                    Distance = 6.06,
                },
                new Race()
                {
                    RaceId = 13,
                    Title = "Patronus 5k",
                    CharityName = "Noah’s Light Foundation's Noah's Gifts",
                    CharityDescription = "A family support program which pays travel expenses for families whose children are undergoing clinical trials for the NOAH Protocol, a revolutionary new treatment for pediatric brain cancer!",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 14,
                    Title = "Marauder’s Challenge",
                    CharityName = "2015",
                    CharityDescription = "All Proceeds from the Marauder's Challenge were divided up evenly amongst all HRC 2015 Charity partners!",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 15,
                    Title = "The Molly Weasley Ugly Jumper Run",
                    CharityName = "One Warm Coat",
                    CharityDescription = "Their mission is to provide anyone in need with a warm coat, free of charge. Your support helped their efforts organizing more than 3,000 coat drives each year and purchasing brand new coats for kids in need.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 16,
                    Title = "Umbridge Run 13k",
                    CharityName = "My Stuff Bags Foundation",
                    CharityDescription = "They provide abused, neglected, and abandoned children entering foster care, with little or no belongings, a My Stuff Bag — an individual duffel bag filled with brand new childhood necessities. HRC helped My Stuff Bags send their 500,000th bag to a child in need!",
                    Distance = 8.08,
                },
                new Race()
                {
                    RaceId = 17,
                    Title = "The Half-Blood Prince Half Marathon",
                    CharityName = "Project Purple",
                    CharityDescription = "A team of runners dedicated to defeating pancreatic cancer, raising awareness, and supporting patients and families affected by this terrible disease that kills more than 80% of those diagnosed.",
                    Distance = 20.92,
                },
                new Race()
                {
                    RaceId = 18,
                    Title = "The #OneHRC House Marathon",
                    CharityName = "Big Cat Rescue, International Bird Rescue, Friends of Scales Reptile Rescue, Save Me Trust",
                    CharityDescription = "The Gryffindor 21k will help Big Cat Rescue in Tampa, Florida, build a new bobcat rehabilitation facility that minimizes human contact so these big cats can return to the wild. The Ravenclaw 10k will help International Bird Rescue in San Francisco, California, build a new aviary to help save birds who are the victims of oil spills and natural disasters. The Slytherin 6k will help Friends of Scales Reptile Rescue outside of Chicago, Illinois, build their reptile rescue facility and provide life - saving veterinary care for abandoned and injured snakes, turtles, and other reptiles. The Hufflepuff 5k will help the Save Me Trust outside of London, England, organize three teams in Southern England to catch, vaccinate and release badgers to stop the spread of bovine tuberculosis and, more importantly, stop the Badger Cull which has killed thousands of badgers.",
                Distance = 26.1,
                },
                new Race()
                {
                    RaceId = 19,
                    Title = "Fantastic Beast 5k",
                    CharityName = "End7",
                    CharityDescription = "While some beasts are fantastic, others are not. Currently, more than 1 billion people around the globe, half of which are children, suffer from seven “neglected tropical diseases” (NTDs). Your participation will support the End7 campaign to rid the developing world of these seven parasitic diseases and end the suffering of over a half a billion suffering children. With your help, we targeted these parasitic “beasts” in at least three post-Ebola countries in West Africa.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 20,
                    Title = "Platform 9 3/4k Year Three",
                    CharityName = "Carry The Future",
                    CharityDescription = "Carry the Future provides infant and toddler carriers to mothers and fathers fleeing war-ravaged regions simply looking for safety for their families. They have donated thousands of carriers and countless aid items to date, and they have a team of volunteers on the ground living near the refugee camps to ensure that they have a constant presence and are aware of immediate needs.",
                    Distance = 6.06,
                },
                new Race()
                {
                    RaceId = 21,
                    Title = "King's Cross Challenge",
                    CharityName = "HRC 2016 Charity Partners",
                    CharityDescription = "The King's Cross Challenge supports all 2016 charity partners, with proceeds from this medal being divided evenly amongst them!",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 22,
                    Title = "Constant Vigilance 5k",
                    CharityName = "Limbs for Life Foundation",
                    CharityDescription = "Limbs for Life Foundation is a global nonprofit organization dedicated to providing fully-functional prosthetic care for individuals who cannot otherwise afford it and raising awareness of the challenges facing amputees. When a person becomes an amputee, they are faced with staggering emotional and financial lifestyle changes.  Fortunately, high tech prosthetic devices that restore a person’s basic skills and independence are available. Unfortunately, many amputees lack the financial resources to obtain adequate prosthetic care. Limbs for Life Foundation steps in to provide a financial bridge between low-income amputees and the quality prosthetic care needed to restore their lives.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 23,
                    Title = "The 10K and S.P.E.W.",
                    CharityName = "Harry Potter Alliance (HPA)",
                    CharityDescription = "Hermione just wanted to do some good in the world. She was determined to make life better for House Elves working in Hogwarts Castle, and throughout the wizarding world. Her compassion for their well-being and her support of their freedom demonstrated her leadership abilities, and led to the elves joining in the fight to defend Hogwarts in the final battle of the Wizarding War. It is in this spirit that proceeds from this event went to support the Harry Potter Alliance and their Granger Leadership Academy. The Harry Potter Alliance (HPA) believes that the world needs more Hermione Grangers, and we emphatically agree.",
                    Distance = 6.2,
                },
                new Race()
                {
                    RaceId = 24,
                    Title = "Unmasked 10-Mile Run For Your Life",
                    CharityName = "To Write Love On Her Arms",
                    CharityDescription = "a charity dedicated to presenting hope and finding help for people struggling with depression, addiction, self-injury, and suicide. TWLOHA exists to encourage, inform, inspire, and also to invest directly into treatment and recovery.",
                    Distance = 6.2,
                },
                new Race()
                {
                    RaceId = 25,
                    Title = "Sirius Half Marathon",
                    CharityName = "Mission K9 Rescue",
                    CharityDescription = "MK9R’s mission is to rescue, reunite, re-home, rehabilitate, and repair any retired working dog that has served mankind in some capacity. They believe these heroic canines have earned and deserve a good retirement, a loving home, and lifelong care. They provide retiring working dog heroes with transport, adoption facilitation, rehoming, financial assistance for life threatening medical situations, and end-of-life benefits which are currently lacking when these dogs retire after serving their country and their communities.",
                    Distance = 13.1,
                },
                new Race()
                {
                    RaceId = 26,
                    Title = "Eternal Glory 4 Miler",
                    CharityName = "Back on My Feet (BoMF)",
                    CharityDescription = "If you think a dragon, a lake and a maze are tough, think about the difficulties of overcoming homelessness. BoMF’s mission is to revolutionize the way our society approaches homelessness. Their unique running-based model demonstrates that if an indivudual first restores confidence, strength and self-esteem, they are better equipped to tackle the road ahead and move toward jobs, homes and new lives. BoMF provides practical training and employment resources for achieving independence, an environment that promotes accountability, and a community that offers compassion and hope for all in need.",
                    Distance = 4.0,
                },
                new Race()
                {
                    RaceId = 27,
                    Title = "Platform 9 3/4k Year Four",
                    CharityName = "Antarctic & Southern Ocean Coalition (ASOC)",
                    CharityDescription = "ASOC is a global network of conservation organizations seeking to preserve the world’s last great wilderness, which belongs to all humankind. ASOC’s mission is to protect the Antarctic and Southern Ocean’s unique and vulnerable ecosystems by speaking for Antarctica and its magnificent species, from the tiniest krill to the largest blue whales.",
                    Distance = 6.06,
                },
                new Race()
                {
                    RaceId = 28,
                    Title = "Quidditch Challenge",
                    CharityName = "HRC 2017 Charity Partners",
                    CharityDescription = "The Quidditch Challenge supports all 2017 charity partners, with proceeds from this medal being divided evenly amongst them!",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 29,
                    Title = "Nargle 9k",
                    CharityName = "Broadway Cares/Equity Fights AIDS (BC/EFA)",
                    CharityDescription = "BC/EFA helps men, women and children across the country and across the street receive lifesaving medications, health care, nutritious meals, counseling and emergency financial assistance. In the ultimate example of Transfiguration in the Muggle World, they turn love into money…and then turn money into love.",
                    Distance = 5.6,
                },
                new Race()
                {
                    RaceId = 30,
                    Title = "DA 5k",
                    CharityName = "Debate Mate",
                    CharityDescription = "Many young people face an uncertain future simply because they are growing up in poverty. By the age of seven, children growing up in poverty are more than twice as likely as their better-off peers to be behind on expected reading levels. By the time they are in their teens, 60 percent won’t get the grades needed to go on to college or get employment after graduation. Debate Mate seeks to reverse that trend by using the power of debate to give disadvantaged young people the skills they need to become exceptional young leaders – confidence, interpersonal communication skills and higher order thinking. While starting in the UK, Debate Mate now enjoys a strong global presence, operating programs in Nepal, Africa, Jamaica, the Middle East, and the United States. They believe that every child deserves equal access to a top education, and wants the youth of today to find their voice so that they can become the leaders of tomorrow.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 31,
                    Title = "Forgotten 5k",
                    CharityName = "Hispanic Federation and Lin-Manuel Miranda’s Immigrants: We Get the Job Done Coalition - Keeping Families Together Initiative",
                    CharityDescription = "All Coalition members work to keep immigrant children and families safe and together. This is a huge undertaking, since most anti-immigrant actions affect not just individuals, but also their family members. Through the Coalition’s ‘Keeping Families Together Initiative’, members provide immigration-related services such as legal and social services to immigrants and their families.",
                    Distance = 3.1,
                },
                new Race()
                {
                    RaceId = 32,
                    Title = "Half-Giant Half Marathon",
                    CharityName = "Toucan Rescue Ranch and Goats of Anarchy",
                    CharityDescription = "Toucan Rescue Ranch (TRR) focuses on the care, rehabilitation and study of Costa Rican wildlife including toucans, owls and sloths! TRR receives and cares for confiscated, sick and injured animals and then gives them a loving home with the goal of providing appropriate medical treatment, rehabilitation and then, when possible, returning them to their natural environment. HRC’s goal was to fully fund a brand new sloth rescue/reception facility in northern Costa Rica. Goats of Anarchy (GOA) is the passion of Leanne Lauricella, who left behind a successful career in New York City and has now created a sanctuary for goats with special needs, teaching the world acceptance and inclusion and inspiring others to “fight like a goat”! Specifically, HRC funded critical fencing and other important infrastructure improvements at GOA’s new facility in New Jersey.",
                    Distance = 13.1,
                },
                new Race()
                {
                    RaceId = 33,
                    Title = "Royal 10k",
                    CharityName = "All Star Code",
                    CharityDescription = "All Star Code is a nonprofit computer science education organization focused on giving young, intelligent, driven men of color access to the necessary skills so they can be part of the growing tech world. HRC’s goal was to help All Star Code fund an entire “cohort” of 20 young men of color so they can complete the Summer Intensive Program in 2019.",
                    Distance = 6.2,
                },
                new Race()
                {
                    RaceId = 34,
                    Title = "Platform 9 3/4k Year Five",
                    CharityName = "RODS – Racing for Orphans with Down Syndrome",
                    CharityDescription = "RODS’ mission is simple. They promote the adoption of orphans with Down syndrome by raising adoption grant funds, one child at a time, and by participating in organized, athletic races and awareness events. They literally run for orphans! The funds they raise cover a large portion of the expense to get children from orphanages in the developing world to forever homes in the US and Canada. Based on the Headmaster’s arithmancy, this event set out to fund seven fantastic kids, and vastly improve their odds of adoption.",
                    Distance = 6.2,
                },
                new Race()
                {
                    RaceId = 35,
                    Title = "Phoenix Challenge",
                    CharityName = "HRC 2018 Charity Partners",
                    CharityDescription = "The Phoenix Challenge supports all 2017 charity partners, with proceeds from this medal being divided evenly amongst them!",
                    Distance = 3.1,
                }
            );

            modelBuilder.Entity<UserRace>().HasData(
                new UserRace() 
                { 
                    UserRaceId = 1,
                    UserId = user.Id,
                    RaceId = 1
                },
                new UserRace()
                {
                    UserRaceId = 2,
                    UserId = user.Id,
                    RaceId = 2
                },
                new UserRace()
                {
                    UserRaceId = 3,
                    UserId = user.Id,
                    RaceId = 3
                },
                new UserRace()
                {
                    UserRaceId = 4,
                    UserId = user.Id,
                    RaceId = 4
                },
                new UserRace()
                {
                    UserRaceId = 5,
                    UserId = user.Id,
                    RaceId = 5
                },
                new UserRace()
                {
                    UserRaceId = 6,
                    UserId = user.Id,
                    RaceId = 6
                }
            );

            modelBuilder.Entity<Topic>().HasData(
                new Topic() 
                { 
                    TopicId = 1,
                    TopicCategoryId = 10,
                    UserId = user.Id,
                    TotalViews = 0,
                    HouseExclusive = false,
                    Title = "J.K. Rowling to pen Adult Potter Novels!?",
                    Content = "There is a rumor going around that once JK finishes writing the Fantastic Beasts Scripts, she will begin writing adult Potter novels! The rumor also claims that it will pretain to Harry being an Auror, Cursed Child being retconned, and somehow tie into the Fantastic Beast series. I believe that something may be going on with the whole obscurus/voldemort connection!"
                },
                new Topic()
                {
                    TopicId = 2,
                    TopicCategoryId = 2,
                    UserId = user.Id,
                    TotalViews = 0,
                    HouseExclusive = true,
                    Title = "Aruleus Dumbledore is the Obscurus or Ariana!",
                    Content = "This is the only thing that makes sense! Discuss!"
                }
            );

        }
    }
}
