using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HogwartsRunningClub.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    HouseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    TotalMiles = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.HouseId);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    RaceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    CharityName = table.Column<string>(nullable: false),
                    CharityDescription = table.Column<string>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    MedalImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.RaceId);
                });

            migrationBuilder.CreateTable(
                name: "TopicCategory",
                columns: table => new
                {
                    TopicCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Label = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicCategory", x => x.TopicCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateRegistered = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()"),
                    HouseId = table.Column<int>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    MilesRun = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 65, nullable: false),
                    Content = table.Column<string>(maxLength: 1500, nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    TotalViews = table.Column<int>(nullable: false),
                    HouseExclusive = table.Column<bool>(nullable: false),
                    TopicCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topic_TopicCategory_TopicCategoryId",
                        column: x => x.TopicCategoryId,
                        principalTable: "TopicCategory",
                        principalColumn: "TopicCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topic_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRace",
                columns: table => new
                {
                    UserRaceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    RaceId = table.Column<int>(nullable: false),
                    RaceBib = table.Column<string>(nullable: true),
                    RaceBibImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRace", x => x.UserRaceId);
                    table.ForeignKey(
                        name: "FK_UserRace_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRace_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    WorkoutId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.WorkoutId);
                    table.ForeignKey(
                        name: "FK_Workout_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UserId = table.Column<string>(nullable: false),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "DateRegistered", "FirstName", "HouseId", "ImagePath", "LastName", "Location", "MilesRun" },
                values: new object[] { "68be530e-98af-4a45-95b5-15d9160af879", 0, "c70ee41e-34da-43ff-bb2b-803a686db1e1", "ApplicationUser", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAENl8AtMvtVhWK0RHKBX44oApIhtIGruMan//T8BNcxnmO3wyePQZYkMa6h6+P1PenA==", null, false, "a65eecf7-9650-453f-8d28-c318933813b9", false, "Admina Straytor", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", null, null, "admin", "Nashville, TN", 134.8 });

            migrationBuilder.InsertData(
                table: "House",
                columns: new[] { "HouseId", "Description", "Title", "TotalMiles" },
                values: new object[,]
                {
                    { 1, "Gryffindor House is named after Hogwarts Founder Godric Gryffindor and is home to young witchs and wizards that model exemplary courage, bravery, and nerve!", "Gryffindor", 0.0 },
                    { 2, "Hufflepuff House is named after Hogwarts Founder Helga Hufflepuff and is home to young witchs and wizards that model honesty, loyalty, justice, and patience!", "Hufflepuff", 0.0 },
                    { 3, "Ravenclaw House is named after Hogwarts Founder Rowena Ravenclaw and is home to young witchs and wizards that model exemplary intelligence, creativity, wit, and learning!", "Ravenclaw", 0.0 },
                    { 4, "Slytherin House is named after Hogwarts Founder Salazar Slytherin and is home to young witchs and wizards that model ambition, cunning, shrewdness, and self-preservation!", "Slytherin", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Race",
                columns: new[] { "RaceId", "CharityDescription", "CharityName", "Distance", "MedalImage", "Title" },
                values: new object[,]
                {
                    { 21, "The King's Cross Challenge supports all 2016 charity partners, with proceeds from this medal being divided evenly amongst them!", "HRC 2016 Charity Partners", 3.1, null, "King's Cross Challenge" },
                    { 22, "Limbs for Life Foundation is a global nonprofit organization dedicated to providing fully-functional prosthetic care for individuals who cannot otherwise afford it and raising awareness of the challenges facing amputees. When a person becomes an amputee, they are faced with staggering emotional and financial lifestyle changes.  Fortunately, high tech prosthetic devices that restore a person’s basic skills and independence are available. Unfortunately, many amputees lack the financial resources to obtain adequate prosthetic care. Limbs for Life Foundation steps in to provide a financial bridge between low-income amputees and the quality prosthetic care needed to restore their lives.", "Limbs for Life Foundation", 3.1, null, "Constant Vigilance 5k" },
                    { 23, "Hermione just wanted to do some good in the world. She was determined to make life better for House Elves working in Hogwarts Castle, and throughout the wizarding world. Her compassion for their well-being and her support of their freedom demonstrated her leadership abilities, and led to the elves joining in the fight to defend Hogwarts in the final battle of the Wizarding War. It is in this spirit that proceeds from this event went to support the Harry Potter Alliance and their Granger Leadership Academy. The Harry Potter Alliance (HPA) believes that the world needs more Hermione Grangers, and we emphatically agree.", "Harry Potter Alliance (HPA)", 6.2, null, "The 10K and S.P.E.W." },
                    { 24, "a charity dedicated to presenting hope and finding help for people struggling with depression, addiction, self-injury, and suicide. TWLOHA exists to encourage, inform, inspire, and also to invest directly into treatment and recovery.", "To Write Love On Her Arms", 6.2, null, "Unmasked 10-Mile Run For Your Life" },
                    { 25, "MK9R’s mission is to rescue, reunite, re-home, rehabilitate, and repair any retired working dog that has served mankind in some capacity. They believe these heroic canines have earned and deserve a good retirement, a loving home, and lifelong care. They provide retiring working dog heroes with transport, adoption facilitation, rehoming, financial assistance for life threatening medical situations, and end-of-life benefits which are currently lacking when these dogs retire after serving their country and their communities.", "Mission K9 Rescue", 13.1, null, "Sirius Half Marathon" },
                    { 26, "If you think a dragon, a lake and a maze are tough, think about the difficulties of overcoming homelessness. BoMF’s mission is to revolutionize the way our society approaches homelessness. Their unique running-based model demonstrates that if an indivudual first restores confidence, strength and self-esteem, they are better equipped to tackle the road ahead and move toward jobs, homes and new lives. BoMF provides practical training and employment resources for achieving independence, an environment that promotes accountability, and a community that offers compassion and hope for all in need.", "Back on My Feet (BoMF)", 4.0, null, "Eternal Glory 4 Miler" },
                    { 27, "ASOC is a global network of conservation organizations seeking to preserve the world’s last great wilderness, which belongs to all humankind. ASOC’s mission is to protect the Antarctic and Southern Ocean’s unique and vulnerable ecosystems by speaking for Antarctica and its magnificent species, from the tiniest krill to the largest blue whales.", "Antarctic & Southern Ocean Coalition (ASOC)", 6.06, null, "Platform 9 3/4k Year Four" },
                    { 30, "Many young people face an uncertain future simply because they are growing up in poverty. By the age of seven, children growing up in poverty are more than twice as likely as their better-off peers to be behind on expected reading levels. By the time they are in their teens, 60 percent won’t get the grades needed to go on to college or get employment after graduation. Debate Mate seeks to reverse that trend by using the power of debate to give disadvantaged young people the skills they need to become exceptional young leaders – confidence, interpersonal communication skills and higher order thinking. While starting in the UK, Debate Mate now enjoys a strong global presence, operating programs in Nepal, Africa, Jamaica, the Middle East, and the United States. They believe that every child deserves equal access to a top education, and wants the youth of today to find their voice so that they can become the leaders of tomorrow.", "Debate Mate", 3.1, null, "DA 5k" },
                    { 29, "BC/EFA helps men, women and children across the country and across the street receive lifesaving medications, health care, nutritious meals, counseling and emergency financial assistance. In the ultimate example of Transfiguration in the Muggle World, they turn love into money…and then turn money into love.", "Broadway Cares/Equity Fights AIDS (BC/EFA)", 5.6, null, "Nargle 9k" },
                    { 19, "While some beasts are fantastic, others are not. Currently, more than 1 billion people around the globe, half of which are children, suffer from seven “neglected tropical diseases” (NTDs). Your participation will support the End7 campaign to rid the developing world of these seven parasitic diseases and end the suffering of over a half a billion suffering children. With your help, we targeted these parasitic “beasts” in at least three post-Ebola countries in West Africa.", "End7", 3.1, null, "Fantastic Beast 5k" },
                    { 31, "All Coalition members work to keep immigrant children and families safe and together. This is a huge undertaking, since most anti-immigrant actions affect not just individuals, but also their family members. Through the Coalition’s ‘Keeping Families Together Initiative’, members provide immigration-related services such as legal and social services to immigrants and their families.", "Hispanic Federation and Lin-Manuel Miranda’s Immigrants: We Get the Job Done Coalition - Keeping Families Together Initiative", 3.1, null, "Forgotten 5k" },
                    { 32, "Toucan Rescue Ranch (TRR) focuses on the care, rehabilitation and study of Costa Rican wildlife including toucans, owls and sloths! TRR receives and cares for confiscated, sick and injured animals and then gives them a loving home with the goal of providing appropriate medical treatment, rehabilitation and then, when possible, returning them to their natural environment. HRC’s goal was to fully fund a brand new sloth rescue/reception facility in northern Costa Rica. Goats of Anarchy (GOA) is the passion of Leanne Lauricella, who left behind a successful career in New York City and has now created a sanctuary for goats with special needs, teaching the world acceptance and inclusion and inspiring others to “fight like a goat”! Specifically, HRC funded critical fencing and other important infrastructure improvements at GOA’s new facility in New Jersey.", "Toucan Rescue Ranch and Goats of Anarchy", 13.1, null, "Half-Giant Half Marathon" },
                    { 33, "All Star Code is a nonprofit computer science education organization focused on giving young, intelligent, driven men of color access to the necessary skills so they can be part of the growing tech world. HRC’s goal was to help All Star Code fund an entire “cohort” of 20 young men of color so they can complete the Summer Intensive Program in 2019.", "All Star Code", 6.2, null, "Royal 10k" },
                    { 34, "RODS’ mission is simple. They promote the adoption of orphans with Down syndrome by raising adoption grant funds, one child at a time, and by participating in organized, athletic races and awareness events. They literally run for orphans! The funds they raise cover a large portion of the expense to get children from orphanages in the developing world to forever homes in the US and Canada. Based on the Headmaster’s arithmancy, this event set out to fund seven fantastic kids, and vastly improve their odds of adoption.", "RODS – Racing for Orphans with Down Syndrome", 6.2, null, "Platform 9 3/4k Year Five" },
                    { 35, "The Phoenix Challenge supports all 2017 charity partners, with proceeds from this medal being divided evenly amongst them!", "HRC 2018 Charity Partners", 3.1, null, "Phoenix Challenge" },
                    { 28, "The Quidditch Challenge supports all 2017 charity partners, with proceeds from this medal being divided evenly amongst them!", "HRC 2017 Charity Partners", 3.1, null, "Quidditch Challenge" },
                    { 18, "The Gryffindor 21k will help Big Cat Rescue in Tampa, Florida, build a new bobcat rehabilitation facility that minimizes human contact so these big cats can return to the wild. The Ravenclaw 10k will help International Bird Rescue in San Francisco, California, build a new aviary to help save birds who are the victims of oil spills and natural disasters. The Slytherin 6k will help Friends of Scales Reptile Rescue outside of Chicago, Illinois, build their reptile rescue facility and provide life - saving veterinary care for abandoned and injured snakes, turtles, and other reptiles. The Hufflepuff 5k will help the Save Me Trust outside of London, England, organize three teams in Southern England to catch, vaccinate and release badgers to stop the spread of bovine tuberculosis and, more importantly, stop the Badger Cull which has killed thousands of badgers.", "Big Cat Rescue, International Bird Rescue, Friends of Scales Reptile Rescue, Save Me Trust", 26.1, null, "The #OneHRC House Marathon" },
                    { 20, "Carry the Future provides infant and toddler carriers to mothers and fathers fleeing war-ravaged regions simply looking for safety for their families. They have donated thousands of carriers and countless aid items to date, and they have a team of volunteers on the ground living near the refugee camps to ensure that they have a constant presence and are aware of immediate needs.", "Carry The Future", 6.06, null, "Platform 9 3/4k Year Three" },
                    { 16, "They provide abused, neglected, and abandoned children entering foster care, with little or no belongings, a My Stuff Bag — an individual duffel bag filled with brand new childhood necessities. HRC helped My Stuff Bags send their 500,000th bag to a child in need!", "My Stuff Bags Foundation", 8.08, null, "Umbridge Run 13k" },
                    { 1, "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.", "The Dana-Farber Cancer Institute / Jimmy Fund", 3.1, null, "Sorcerer's Stone 5k" },
                    { 2, "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.", "The Dana-Farber Cancer Institute / Jimmy Fund", 6.2, null, "Chamber of Secrets 10k" },
                    { 3, "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.", "The Dana-Farber Cancer Institute / Jimmy Fund", 3.1, null, "A Walk In the Moonlight 5k" },
                    { 17, "A team of runners dedicated to defeating pancreatic cancer, raising awareness, and supporting patients and families affected by this terrible disease that kills more than 80% of those diagnosed.", "Project Purple", 20.92, null, "The Half-Blood Prince Half Marathon" },
                    { 5, "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.", "The Dana-Farber Cancer Institute / Jimmy Fund", 6.06, null, "Platform 9 3/4k Year One" },
                    { 6, "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.", "The Dana-Farber Cancer Institute / Jimmy Fund", 18.64, null, "Triwizard Challenge 5k/10k/15k" },
                    { 7, "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.", "The Dana-Farber Cancer Institute / Jimmy Fund", 3.1, null, "Deathly Hallows Challenge" },
                    { 8, "An Atlanta-based running organization that raises awareness of CF, sponsors CF research, and directly supports local families dealing with this terrible disease. Their youth organization, Rosebuds, is a youth running club founded by a brave little girl named Elana with the theme of “Kids helping kids with CF.", "Miles for Cystic Fibrosis", 3.1, null, "Dementor's Kiss 5k" },
                    { 4, "Since its founding in 1947, Dana-Farber Cancer Institute in Boston, Massachusetts has been committed to providing adults and children with cancer with the best treatment available today while developing tomorrow's cures through cutting-edge research.", "The Dana-Farber Cancer Institute / Jimmy Fund", 3.1, null, "Hedwig Memorial Run 5k" },
                    { 10, "An incredible organization with one simple mission: To enable people with all types of disabilities to participate in mainstream athletics in order to promote personal achievement, enhance self esteem, and lower barriers to living a fulfilling life. Whether it’s supplying racing wheelchairs to athletes or connecting a guide with a vision-impaired runner, Achilles International ensures that an athlete’s disability doesn't limit their potential.", "Achilles International", 5.0, null, "Voldemort V-Miler" },
                    { 11, "A Florida-based group of firefighters, police officers and EMTs who ride bicycles to honor those who have died in the line of duty. These volunteers ride hundreds of miles on or near the anniversary of the honoree’s ultimate sacrifice to remind the families, friends, and the community of the fallen that they are not forgotten. The result of their rides, and this event, were donations made directly to the families of those who gave their lives to protect the rest of us.", "Brotherhood Ride", 3.88, null, "Dept. of Mysteries 6.2442k" },
                    { 12, "Accio Books! is the Harry Potter Alliance’s annual book drive and since 2009, Harry Potter fans around the world have donated more than 350,000 books to underprivileged or under-served readers.", "Harry Potter Alliance Accio Books! Campaign", 6.06, null, "Platform 9 3/4k Year Two" },
                    { 13, "A family support program which pays travel expenses for families whose children are undergoing clinical trials for the NOAH Protocol, a revolutionary new treatment for pediatric brain cancer!", "Noah’s Light Foundation's Noah's Gifts", 3.1, null, "Patronus 5k" },
                    { 14, "All Proceeds from the Marauder's Challenge were divided up evenly amongst all HRC 2015 Charity partners!", "2015", 3.1, null, "Marauder’s Challenge" },
                    { 15, "Their mission is to provide anyone in need with a warm coat, free of charge. Your support helped their efforts organizing more than 3,000 coat drives each year and purchasing brand new coats for kids in need.", "One Warm Coat", 3.1, null, "The Molly Weasley Ugly Jumper Run" },
                    { 9, "An awesome charity that connects military members with loving foster homes to care for their pets when they have to deploy overseas. They also provide financial assistance for emergency medical services for military pets.", "Dogs on Deployment", 3.1, null, "Weasley’s Wizarding Wheezer 5k" }
                });

            migrationBuilder.InsertData(
                table: "TopicCategory",
                columns: new[] { "TopicCategoryId", "Label" },
                values: new object[,]
                {
                    { 8, "Books" },
                    { 7, "Humor" },
                    { 6, "Personal" },
                    { 5, "Fitness" },
                    { 2, "Lore" },
                    { 3, "Charity" },
                    { 1, "General" },
                    { 9, "Movies" },
                    { 4, "Races" },
                    { 10, "Rumors" }
                });

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "TopicId", "Content", "DateCreated", "HouseExclusive", "Title", "TopicCategoryId", "TotalViews", "UserId" },
                values: new object[,]
                {
                    { 2, "This is the only thing that makes sense! Discuss!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Aruleus Dumbledore is the Obscurus or Ariana!", 2, 0, "68be530e-98af-4a45-95b5-15d9160af879" },
                    { 1, "There is a rumor going around that once JK finishes writing the Fantastic Beasts Scripts, she will begin writing adult Potter novels! The rumor also claims that it will pretain to Harry being an Auror, Cursed Child being retconned, and somehow tie into the Fantastic Beast series. I believe that something may be going on with the whole obscurus/voldemort connection!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "J.K. Rowling to pen Adult Potter Novels!?", 10, 0, "68be530e-98af-4a45-95b5-15d9160af879" }
                });

            migrationBuilder.InsertData(
                table: "UserRace",
                columns: new[] { "UserRaceId", "RaceBib", "RaceBibImage", "RaceId", "UserId" },
                values: new object[,]
                {
                    { 1, null, null, 1, "68be530e-98af-4a45-95b5-15d9160af879" },
                    { 2, null, null, 2, "68be530e-98af-4a45-95b5-15d9160af879" },
                    { 3, null, null, 3, "68be530e-98af-4a45-95b5-15d9160af879" },
                    { 4, null, null, 4, "68be530e-98af-4a45-95b5-15d9160af879" },
                    { 5, null, null, 5, "68be530e-98af-4a45-95b5-15d9160af879" },
                    { 6, null, null, 6, "68be530e-98af-4a45-95b5-15d9160af879" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HouseId",
                table: "AspNetUsers",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TopicId",
                table: "Comment",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_TopicCategoryId",
                table: "Topic",
                column: "TopicCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_UserId",
                table: "Topic",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRace_RaceId",
                table: "UserRace",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRace_UserId",
                table: "UserRace",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_UserId",
                table: "Workout",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "UserRace");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "TopicCategory");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "House");
        }
    }
}
