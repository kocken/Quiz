using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Quiz.API.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("https://localhost:44362")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddSingleton(new QuestionList(GetQuestions()));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private Question[] GetQuestions()
        {
            var questions = new List<Question>();

            var jArray = JArray.Parse(File.ReadAllText("questions_list.json"));

            foreach (var element in jArray)
            {
                var id = element["Id"].Value<int>();
                var message = element["Question"].Value<string>();

                var answers = new Answer[] {
                    new Answer('A', element["A"].Value<string>()),
                    new Answer('B', element["B"].Value<string>()),
                    new Answer('C', element["C"].Value<string>())
                };

                var correctAnswer = element["Correct"].Value<char>();

                questions.Add(new Question(id, message, answers, correctAnswer));
            }

            return questions.ToArray();
        }
    }
}
