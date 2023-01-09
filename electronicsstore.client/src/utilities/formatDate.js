export function formatDate(date) {
  if (date == null)
    return;

  const d = new Date(date);
  return d.getFullYear() + "-"
    + ("0" + d.getDate()).slice(-2) + "-"
    + ("0" + (d.getMonth()+1)).slice(-2) + " "
    + ("0" + d.getHours()).slice(-2) + ":"
    + ("0" + d.getMinutes()).slice(-2) + ":"
    + ("0" + d.getSeconds()).slice(-2);
}