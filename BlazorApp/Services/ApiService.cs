using DtoModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.JSInterop;
using System;
using System.Drawing;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace BlazorApp.Services
{
    public class ApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        // pacientes
        public async Task<List<PacienteDto>> GetPacientes()
        {
            var pacientes = await httpClient.GetFromJsonAsync<List<PacienteDto>>("api/pacientes");
            return pacientes ?? new List<PacienteDto>();
        }

        public async Task<PacienteDto> GetPaciente(int id)
        {
            var paciente = await httpClient.GetFromJsonAsync<PacienteDto>($"api/pacientes/{id}");
            return paciente;
        }

        public async Task<PacienteDto> AddPaciente(PacienteDto paciente)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await httpClient.PostAsJsonAsync("api/pacientes", paciente);
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var pacienteDto = JsonSerializer.Deserialize<PacienteDto>(content, options);
                return pacienteDto;
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public async Task<PacienteDto?> UpdatePaciente(int id, PacienteDto paciente)
        {
            var response = await httpClient.PutAsJsonAsync($"api/pacientes/{id}", paciente);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PacienteDto>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeletePaciente(int id)
        {
            var response = await httpClient.DeleteAsync($"api/pacientes/{id}");

            return response.IsSuccessStatusCode;
        }

        // consultas
        public async Task<List<ConsultaDto>> GetConsultas()
        {
            var consultas = await httpClient.GetFromJsonAsync<List<ConsultaDto>>("api/consultas");
            return consultas ?? new List<ConsultaDto>();
        }

        public async Task<ConsultaDto> GetConsulta(int id)
        {
            var consulta = await httpClient.GetFromJsonAsync<ConsultaDto>($"api/consultas/{id}");
            return consulta;
        }

        public async Task<ConsultaDto> AddConsulta(ConsultaDto consulta)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await httpClient.PostAsJsonAsync("api/consultas", consulta);
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var consultaDto = JsonSerializer.Deserialize<ConsultaDto>(content, options);
                return consultaDto;
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public async Task<ConsultaDto?> UpdateConsulta(int id, ConsultaDto consulta)
        {
            var response = await httpClient.PutAsJsonAsync($"api/consultas/{id}", consulta);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ConsultaDto>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteConsulta(int id)
        {
            var response = await httpClient.DeleteAsync($"api/consultas/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task MarcarConsulta(int idConsulta, int idPaciente)
        {
            var consultaPaciente = new Consulta_PacienteDto
            {
                IdConsulta = idConsulta,
                IdPaciente = idPaciente
            };

            await httpClient.PostAsJsonAsync("api/consultas_pacientes", consultaPaciente);
        }

        // consultas_pacientes

        public async Task<List<Consulta_PacienteDto>> GetConsultasPacientes()
        {
            var consultasPacientes = await httpClient.GetFromJsonAsync<List<Consulta_PacienteDto>>("api/consultas_pacientes");
            var pacientes = await httpClient.GetFromJsonAsync<List<PacienteDto>>("api/pacientes");
            var consultas = await httpClient.GetFromJsonAsync<List<ConsultaDto>>("api/consultas");

            foreach (var cp in consultasPacientes)
            {
                var paciente = pacientes.FirstOrDefault(p => p.Num_Processo == cp.IdPaciente);

                var consulta = consultas.FirstOrDefault(c => c.Id == cp.IdConsulta);

                if (paciente != null)
                {
                    cp.NomePaciente = paciente.Nome;
                }

                if (consulta != null)
                {
                    cp.Especialidade = consulta.Especialidade;
                    cp.DataConsulta = consulta.DataConsulta;
                }
            }

            return consultasPacientes ?? new List<Consulta_PacienteDto>();
        }

        public void DownloadPDF(IJSRuntime js, string filename, List<Consulta_PacienteDto> cp)
        {
            js.InvokeVoidAsync("DownloadPDF", filename, Convert.ToBase64String(PDFReport(cp)));
        }

        private byte[] PDFReport(List<Consulta_PacienteDto> cp)
        {
            var memoryStream = new MemoryStream();
            float margeL = 5.5f;
            float margeR = 5.5f;
            float margeT = 1.0f;
            float margeB = 1.0f;

            Document pdf = new Document(
                PageSize.A4,
                margeL,
                margeR,
                margeT,
                margeB
                );

            PdfWriter writer = PdfWriter.GetInstance(pdf, memoryStream);

            // conteúdo
            pdf.Open();

            var titulo = new Paragraph("Relatório de Consulta", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA,20, iTextSharp.text.Font.BOLD));
            titulo.Alignment = Element.ALIGN_CENTER;

            pdf.Add(titulo);

            iTextSharp.text.Font _fontStyle = FontFactory.GetFont("Tahoma", 12f, iTextSharp.text.Font.NORMAL);

            // a consulta no qual eu cliquei
            var conteudoRelatorio = $@"
Número de Processo: {cp[0].IdPaciente}
Paciente: {cp[0].NomePaciente}
ID da Consulta: {cp[0].IdConsulta}
Especialidade: {cp[0].Especialidade}
Data da Consulta: {cp[0].DataConsulta}
Observações: ...
";
            var conteudo1 = new Paragraph(conteudoRelatorio, _fontStyle);
            pdf.Add(conteudo1);

            var titulo2 = new Paragraph("Histórico de Consultas", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 18, iTextSharp.text.Font.BOLD));
            titulo2.Alignment = Element.ALIGN_CENTER;

            pdf.Add(titulo2);

            // consultas anteriores
            var conteudoHistorico = "";
            for (int i = 1; i < cp.Count; i++)
            {
                var consulta = cp[i];
                conteudoHistorico += $@"
Especialidade: {consulta.Especialidade}
Data da Consulta: {consulta.DataConsulta}
";
            }

            var conteudo2 = new Paragraph(conteudoHistorico, _fontStyle);
            pdf.Add(conteudo2);

            pdf.Close();

            return memoryStream.ToArray();
        }
    }
}
