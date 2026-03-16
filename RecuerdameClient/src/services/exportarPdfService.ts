import type { TomaProgramada } from "@/types";
import { HttpService } from "./httpservice";
import jsPDF from 'jspdf'
import autoTable from 'jspdf-autotable'
import type { EstadoToma } from "@/enums/enums";


export class ExportarPdfService extends HttpService {
    private static instance: ExportarPdfService
    private constructor() {
        super();
    }

    public static getInstance(): ExportarPdfService {
        if (!ExportarPdfService.instance) {
            ExportarPdfService.instance = new ExportarPdfService()
        }
        return ExportarPdfService.instance
    }

    async ExportarPdf({     
    tomasRealizadas,
    tomasPendientes,
    tomasCanceladas,
    todasLasTomas,
    data,
    estadoLabel
}: {
    tomasRealizadas: number,
    tomasPendientes: number,
    tomasCanceladas: number,
    todasLasTomas: number,
    data: TomaProgramada[],
    estadoLabel: (estado: EstadoToma) =>  "Tomada" | "Cancelada" | "Pendiente"
}) {
        const doc = new jsPDF()

        // Header azul
        doc.setFillColor(51, 102, 238)
        doc.rect(0, 0, 210, 34, 'F')
        doc.setTextColor(255, 255, 255)
        doc.setFontSize(17)
        doc.setFont('helvetica', 'bold')
        doc.text('Historial de Tomas Programadas', 14, 14)
        doc.setFontSize(9)
        doc.setFont('helvetica', 'normal')
        const hoy = new Date().toLocaleDateString('es-DO', { day: 'numeric', month: 'long', year: 'numeric' })
        doc.text(`Generado el ${hoy}`, 14, 24)

        // Resumen de stats
        doc.setTextColor(80, 80, 110)
        doc.setFontSize(9)
        doc.setFont('helvetica', 'bold')
        doc.text(`Tomadas: ${tomasRealizadas}`, 14, 44)
        doc.text(`Pendientes: ${tomasPendientes}`, 55, 44)
        doc.text(`Canceladas: ${tomasCanceladas}`, 100, 44)
        doc.text(`Total: ${todasLasTomas}`, 150, 44)

        // Tabla
        const filas  = data.map(t => [
            t.medicamentoNombre,
            t.categoriaNombre,
            `${t.dosis} mg`,
            new Date(t.fechaHoraProgramada).toLocaleDateString('es-DO'),
            new Date(t.fechaHoraProgramada).toLocaleTimeString('es-DO', { hour: '2-digit', minute: '2-digit' }),
            estadoLabel(t.estadoToma),
        ])

        autoTable(doc, {
            startY: 50,
            head: [['Medicamento', 'Categoría', 'Dosis', 'Fecha', 'Hora', 'Estado']],
            body: filas,
            headStyles: {
            fillColor: [51, 102, 238],
            textColor: 255,
            fontStyle: 'bold',
            fontSize: 9,
            },
            bodyStyles: {
            fontSize: 8.5,
            textColor: [30, 30, 60],
            },
            alternateRowStyles: {
            fillColor: [238, 244, 255],
            },
            columnStyles: {
            5: { halign: 'center', fontStyle: 'bold' },
            },
            didParseCell: (data) => {
            if (data.column.index === 5 && data.section === 'body') {
                const estado = data.cell.raw as string
                if (estado === 'Tomada') {
                data.cell.styles.textColor = [5, 150, 105]
                data.cell.styles.fillColor = [236, 253, 245]
                } else if (estado === 'Cancelada') {
                data.cell.styles.textColor = [220, 38, 38]
                data.cell.styles.fillColor = [254, 242, 242]
                } else {
                data.cell.styles.textColor = [180, 90, 0]
                data.cell.styles.fillColor = [255, 251, 235]
                }
            }
            },
            margin: { left: 14, right: 14 },
        })

        doc.save('historial-tomas.pdf')
    }
}
 