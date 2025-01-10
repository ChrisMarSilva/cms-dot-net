using Bogus;
using Cache.Web.Consumers;
using Cache.Web.Contracts;
using Cache.Web.Database.Contexts;
using Cache.Web.Models;
using Cache.Web.Services;
using Cache.Web.Workers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Cache.Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCollections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        //services.AddDbSeed();
        services.AddServices();
        services.AddWorkers(configuration);

        return services;
    }

    public static IApplicationBuilder UseCollections(this IApplicationBuilder app, IConfiguration configuration)
    {
        

        return app;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuickGridEntityFrameworkAdapter();
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("MensagensInMemoryDb")); //.UseAsyncSeeding(async (context, _, ct) => { });
  
        return services;
    }

    private static IServiceCollection AddDbSeed(this IServiceCollection services)
    {
        var xmls = new[] {
            @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
  <Envelope xmlns=""https://www.bcb.gov.br/pi/pain.009/1.0"">
    <AppHdr>
      <Fr>
        <FIId>
          <FinInstnId>
            <Othr>
              <Id>00038166</Id>
            </Othr>
          </FinInstnId>
        </FIId>
      </Fr>
      <To>
        <FIId>
          <FinInstnId>
            <Othr>
              <Id>11111111</Id>
            </Othr>
          </FinInstnId>
        </FIId>
      </To>
      <BizMsgIdr>M0003816612345678901234567890123</BizMsgIdr>
      <MsgDefIdr>pain.009.spi.1.0</MsgDefIdr>
      <CreDt>2024-04-30T12:00:00.510Z</CreDt>
      <Sgntr/>
    </AppHdr>
    <Document>
      <MndtInitnReq>
        <GrpHdr>
          <MsgId>M0003816612345678901234567890123</MsgId>
          <CreDtTm>2024-04-30T12:00:00.510Z</CreDtTm>
        </GrpHdr>
        <Mndt>
          <MndtId>RR2222222220240429njua7shf40k</MndtId>
          <MndtReqId>SC2222222220240430np8c7shf40k</MndtReqId>
          <Ocrncs>
            <SeqTp>RCUR</SeqTp>
            <Frqcy>
              <Tp>MNTH</Tp>
            </Frqcy>
            <FrstColltnDt>2024-05-03</FrstColltnDt>
            <FnlColltnDt>2025-05-03</FnlColltnDt>
          </Ocrncs>
          <TrckgInd>false</TrckgInd>
          <ColltnAmt Ccy=""BRL"">100.00</ColltnAmt>
          <Cdtr>
            <Nm>Empresa XPTO</Nm>
            <Id>
              <PrvtId>
                <Othr>
                  <Id>12822222215001</Id>
                </Othr>
              </PrvtId>
            </Id>
          </Cdtr>
          <CdtrAgt>
            <FinInstnId>
              <ClrSysMmbId>
                <MmbId>22222222</MmbId>
              </ClrSysMmbId>
            </FinInstnId>
          </CdtrAgt>
          <Dbtr>
            <Id>
              <PrvtId>
                <Othr>
                  <Id>23933333316</Id>
                </Othr>
              </PrvtId>
            </Id>
          </Dbtr>
          <DbtrAcct>
            <Id>
              <Othr>
                <Id>1111108</Id>
                <Issr>0001</Issr>
              </Othr>
            </Id>
          </DbtrAcct>
          <DbtrAgt>
            <FinInstnId>
              <ClrSysMmbId>
                <MmbId>11111111</MmbId>
              </ClrSysMmbId>
            </FinInstnId>
          </DbtrAgt>
          <UltmtDbtr>
            <Nm>Sicrano da Silva</Nm>
            <Id>
              <PrvtId>
                <Othr>
                  <Id>02822222215</Id>
                </Othr>
              </PrvtId>
            </Id>
          </UltmtDbtr>
          <RfrdDoc>
            <Nb>1234567890</Nb>
            <CdtrRef>ononon ononon</CdtrRef>
          </RfrdDoc>
          <SplmtryData>
            <Envlp>
              <MndtPrcgDtls>
                <MndtPrcgTp>CRTN</MndtPrcgTp>
                <PrcgDtTm>2024-04-29T09:01:00.620Z</PrcgDtTm>
              </MndtPrcgDtls>
              <MndtPrcgDtls>
                <MndtPrcgTp>CRAT</MndtPrcgTp>
                <PrcgDtTm>2024-04-30T12:00:00.000Z</PrcgDtTm>
              </MndtPrcgDtls>
              <MndtPrcgDtls>
                <MndtPrcgTp>EXPR</MndtPrcgTp>
                <PrcgDtTm>2024-04-30T15:00:00.000Z</PrcgDtTm>
              </MndtPrcgDtls>
            </Envlp>
          </SplmtryData>
        </Mndt>
      </MndtInitnReq>
    </Document>
  </Envelope>",
            @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<Envelope xmlns=""https://www.bcb.gov.br/pi/pain.012/1.1"">
  <AppHdr>
    <Fr>
      <FIId>
        <FinInstnId>
          <Othr>
            <Id>11111111</Id>
          </Othr>
        </FinInstnId>
      </FIId>
    </Fr>
    <To>
      <FIId>
        <FinInstnId>
          <Othr>
            <Id>00038166</Id>
          </Othr>
        </FinInstnId>
      </FIId>
    </To>
    <BizMsgIdr>M11111111dfg4frgh342434234324yyh</BizMsgIdr>
    <MsgDefIdr>pain.012.spi.1.1</MsgDefIdr>
    <CreDt>2024-04-30T12:01:00.310Z</CreDt>
    <Sgntr/>
  </AppHdr>
  <Document>
    <MndtAccptncRpt>
      <GrpHdr>
        <MsgId>M11111111dfg4frgh342434234324yyh</MsgId>
        <CreDtTm>2024-04-30T12:01:00.310Z</CreDtTm>
        <InstgAgt>
          <FinInstnId>
            <ClrSysMmbId>
              <MmbId>11111111</MmbId>
            </ClrSysMmbId>
          </FinInstnId>
        </InstgAgt>
      </GrpHdr>
      <UndrlygAccptncDtls>
        <AccptncRslt>
          <Accptd>true</Accptd>
        </AccptncRslt>
        <OrgnlMndt>
          <OrgnlMndt>
            <MndtId>RR2222222220240429njua7shf40k</MndtId>
            <MndtReqId>IS1111111120240430njua7shf40g</MndtReqId>
            <Ocrncs>
              <SeqTp>RCUR</SeqTp>
              <Frqcy>
                <Tp>MNTH</Tp>
              </Frqcy>
              <FrstColltnDt>2024-05-03</FrstColltnDt>
              <FnlColltnDt>2025-05-03</FnlColltnDt>
            </Ocrncs>
            <TrckgInd>false</TrckgInd>
            <ColltnAmt Ccy=""BRL"">100.00</ColltnAmt>
            <Cdtr>
              <Nm>Empresa XPTO</Nm>
              <Id>
                <PrvtId>
                  <Othr>
                    <Id>12822222215001</Id>
                  </Othr>
                </PrvtId>
              </Id>
            </Cdtr>
            <CdtrAgt>
              <FinInstnId>
                <ClrSysMmbId>
                  <MmbId>22222222</MmbId>
                </ClrSysMmbId>
              </FinInstnId>
            </CdtrAgt>
            <Dbtr>
               <Id>
                <PrvtId>
                  <Othr>
                    <Id>23933333316</Id>
                  </Othr>
                </PrvtId>
              </Id>
            </Dbtr>
            <DbtrAcct>
              <Id>
                <Othr>
                  <Id>1111108</Id>
                  <Issr>0001</Issr>
                </Othr>
              </Id>
            </DbtrAcct>
            <DbtrAgt>
              <FinInstnId>
                <ClrSysMmbId>
                  <MmbId>11111111</MmbId>
                </ClrSysMmbId>
              </FinInstnId>
            </DbtrAgt>
            <UltmtDbtr>
              <Nm>Sicrano da Silva</Nm>
              <Id>
                <PrvtId>
                  <Othr>
                    <Id>02822222215</Id>
                  </Othr>
                </PrvtId>
              </Id>
            </UltmtDbtr>
            <RfrdDoc>
              <Nb>1234567890</Nb>
              <CdtrRef>ononon ononon</CdtrRef>
            </RfrdDoc>
          </OrgnlMndt>
        </OrgnlMndt>
        <SplmtryData>
          <Envlp>
            <MndtPrcgDtls>
              <MndtPrcgTp>CRTN</MndtPrcgTp>
              <PrcgDtTm>2024-04-29T09:01:00.620Z</PrcgDtTm>
            </MndtPrcgDtls>
            <MndtPrcgDtls>
              <MndtPrcgTp>UPDT</MndtPrcgTp>
              <PrcgDtTm>2024-04-30T12:01:00.310Z</PrcgDtTm>
            </MndtPrcgDtls>
            <MndtSts>PDNG</MndtSts>
          </Envlp>
        </SplmtryData>
      </UndrlygAccptncDtls>
    </MndtAccptncRpt>
  </Document>
</Envelope>",
            @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<Envelope xmlns=""""https://www.bcb.gov.br/pi/pain.011/1.1"""">
  <AppHdr>
    <Fr>
      <FIId>
        <FinInstnId>
          <Othr>
            <Id>11111111</Id>
          </Othr>
        </FinInstnId>
      </FIId>
    </Fr>
    <To>
      <FIId>
        <FinInstnId>
          <Othr>
            <Id>00038166</Id>
          </Othr>
        </FinInstnId>
      </FIId>
    </To>
    <BizMsgIdr>M11111111rtd4trfh567432907688noh</BizMsgIdr>
    <MsgDefIdr>pain.011.spi.1.1</MsgDefIdr>
    <CreDt>2024-06-29T09:01:00.620Z</CreDt>
    <Sgntr/>
  </AppHdr>
  <Document>
    <MndtCxlReq>
      <GrpHdr>
        <MsgId>M11111111rtd4trfh567432907688noh</MsgId>
        <CreDtTm>2024-06-29T09:01:00.620Z</CreDtTm>
        <InstgAgt>
          <FinInstnId>
            <ClrSysMmbId>
              <MmbId>11111111</MmbId>
            </ClrSysMmbId>
          </FinInstnId>
        </InstgAgt>
      </GrpHdr>
      <UndrlygCxlDtls>
        <CxlRsn>
          <Orgtr>
            <Id>
              <PrvtId>
                <Othr>
                  <Id>23933333316</Id>
                </Othr>
              </PrvtId>
            </Id>
          </Orgtr>
          <Rsn>
            <Prtry>SLDB</Prtry>
          </Rsn>
        </CxlRsn>
        <OrgnlMndt>
          <OrgnlMndt>
            <MndtId>RR2222222220240429njua7shf40k</MndtId>
            <MndtReqId>IC11111111202406292njua7shf40</MndtReqId>
            <Ocrncs>
              <SeqTp>RCUR</SeqTp>
              <Frqcy>
                <Tp>MNTH</Tp>
              </Frqcy>
              <FrstColltnDt>2024-05-01</FrstColltnDt>
              <FnlColltnDt>2025-05-01</FnlColltnDt>
            </Ocrncs>
            <TrckgInd>false</TrckgInd>
            <ColltnAmt Ccy=""""BRL"""">100.00</ColltnAmt>
            <Cdtr>
              <Nm>Empresa XPTO</Nm>
              <Id>
                <PrvtId>
                  <Othr>
                    <Id>12822222215001</Id>
                  </Othr>
                </PrvtId>
              </Id>
            </Cdtr>
            <CdtrAgt>
              <FinInstnId>
                <ClrSysMmbId>
                  <MmbId>22222222</MmbId>
                </ClrSysMmbId>
              </FinInstnId>
            </CdtrAgt>
            <Dbtr>
              <Id>
                <PrvtId>
                  <Othr>
                    <Id>23933333316</Id>
                  </Othr>
                </PrvtId>
              </Id>
            </Dbtr>
            <DbtrAcct>
              <Id>
                <Othr>
                  <Id>1111108</Id>
                  <Issr>0001</Issr>
                </Othr>
              </Id>
            </DbtrAcct>
            <DbtrAgt>
              <FinInstnId>
                <ClrSysMmbId>
                  <MmbId>11111111</MmbId>
                </ClrSysMmbId>
              </FinInstnId>
            </DbtrAgt>
            <UltmtDbtr>
              <Nm>Sicrano da Silva</Nm>
              <Id>
                <PrvtId>
                  <Othr>
                    <Id>02822222215</Id>
                  </Othr>
                </PrvtId>
              </Id>
            </UltmtDbtr>
            <RfrdDoc>
              <Nb>1234567890</Nb>
              <CdtrRef>ononon ononon</CdtrRef>
            </RfrdDoc>
          </OrgnlMndt>
        </OrgnlMndt>
        <SplmtryData>
          <Envlp>
            <MndtPrcgDtls>
              <MndtPrcgTp>CRTN</MndtPrcgTp>
              <PrcgDtTm>2024-04-29T09:01:00.620Z</PrcgDtTm>
            </MndtPrcgDtls>
            <MndtPrcgDtls>
              <MndtPrcgTp>CLTN</MndtPrcgTp>
              <PrcgDtTm>2024-06-29T09:00:00.600Z</PrcgDtTm>
            </MndtPrcgDtls>
          </Envlp>
        </SplmtryData>
      </UndrlygCxlDtls>
    </MndtCxlReq>
  </Document>
</Envelope>"
        };

        var faker = new Faker<MensagemModel>("pt_BR")
            //.StrictMode(true)
            .UseSeed(123)
            .RuleFor(u => u.Id, f => f.Random.Guid()) // Guid.NewGuid()
            .RuleFor(u => u.IdMsgJdPi, f => f.Random.Guid().ToString()) // Guid.NewGuid().ToString()
            .RuleFor(u => u.IdMsg, f => $"M{DateTime.UtcNow:yyyyMMddHHmmss}00000000{f.Random.Number(1, 100):000}MDKGPXJKU3U")
            .RuleFor(x => x.TpMsg, f => f.PickRandom(new[] { "pain.009", "pain.011", "pain.012" }))
            .RuleFor(u => u.QueueMsg, f => "jd.pi.bacen.send.command.queue")
            .RuleFor(u => u.XmlMsg, (f, u) => u.TpMsg == "pain.009" ? xmls[0] : u.TpMsg == "pain.009" ? xmls[1] : xmls[2])
            .RuleFor(u => u.DtHrRegistro, f => DateTime.UtcNow);

        var mensagemModels = faker.Generate(10);

        //var mensagemModels = Enumerable.Range(1, 5)
        //  .Select(i => new MensagemModel
        //  {
        //      Id = Guid.NewGuid(),
        //      IdMsgJdPi = Guid.NewGuid().ToString(),
        //      IdMsg = $"M{DateTime.UtcNow:yyyyMMddHHmmss}00000000{i:000}MDKGPXJKU3U",
        //      TpMsg = "pain.009",
        //      QueueMsg = "jd.pi.bacen.send.command.queue",
        //      XmlMsg = ,
        //      DtHrRegistro = DateTime.UtcNow
        //  })
        //  .ToList();

        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Set<MensagemModel>().AddRange(mensagemModels);
        context.SaveChanges(); // await context.SaveChangesAsync(ct);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<MensagemService>();

        return services;
    }

    private static IServiceCollection AddWorkers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter(); // formatar os nomes de fila para Caso Kebab "MyQueue" -> "my-queue"
            
            x.AddConsumer<MessagemConsumer>();
            //m.AddConsumers(Assembly.GetExecutingAssembly());

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(
                    configuration.GetValue("RabbitMQ:Host", "localhost"),
                    configuration.GetValue<ushort>("Rabbit:Port", 5672),
                    configuration.GetValue("RabbitMQ:VirtualHost", "/"),
                    h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]!);
                        h.Password(configuration["RabbitMQ:Password"]!);
                        h.PublisherConfirmation = configuration.GetValue("Rabbit:PublisherConfirmation", true);
                    });

                cfg.ReceiveEndpoint(configuration["RabbitMQ:Queue"]!, e =>
                {
                    e.ConfigureConsumer<MessagemConsumer>(context);
                    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5))); // Configurar Retry Policy - 3: Número de tentativas - 5s: Intervalo entre cada tentativa.
                    e.UseInMemoryOutbox(context, o => o.ConcurrentMessageDelivery = true);
                    e.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10))); // Dead Letter Queue (DLQ) // Reenvia mensagens após um intervalo
                    e.DiscardSkippedMessages(); // Mensagens falhas são descartadas após retries.
                    e.ConsumerPriority = 5; // configuration.GetValue<ushort>("Rabbit:ConsumerPriority", 5);
                    e.PrefetchCount = 32; // configuration.GetValue<ushort>("Rabbit:PrefetchCount", 32);
                    e.ConfigureConsumeTopology = false; // configuration.GetValue("Rabbit:ConfigureConsumeTopology", true);
                });

                cfg.Message<Fault>(e => e.SetEntityName("queue.fault"));
                cfg.Message<MensagemDto>(e => e.SetEntityName(configuration.GetValue("RabbitMQ:Queue", "queue")));
            });
        });

        services
            .AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(10);
                options.StopTimeout = TimeSpan.FromSeconds(10);
            });

        services.AddHostedService<MessageWorker>();

        return services;
    }

    public static async Task SendAsync<T>(this ISendEndpointProvider sendEndpointProvider, string queue, T message, CancellationToken cancellationToken = default) where T : class
    {
        var endpoint = await sendEndpointProvider
            .GetSendEndpoint(new Uri($"queue:{queue}"))
            .ConfigureAwait(continueOnCapturedContext: false);

        await endpoint
            .Send(message, cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);
    }
}