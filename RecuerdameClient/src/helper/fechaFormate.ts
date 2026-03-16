const fecha = new Date();
const fechaFormateada = fecha.toLocaleDateString('es-Es', {
  day: 'numeric',
  month: 'long',
  year: 'numeric'
})

export default fechaFormateada