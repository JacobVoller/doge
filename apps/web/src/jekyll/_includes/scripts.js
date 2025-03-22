(async function()
{
    const searchInput = document.getElementById('search');
    const categoryFilter = document.getElementById('categoryFilter');
    const tableRows = document.querySelectorAll('#dataTable tbody tr');

    function filterTable() {
      const searchText = searchInput.value.toLowerCase();
      const category = categoryFilter.value;

      tableRows.forEach(row => {
        const cells = row.querySelectorAll('td');
        const name = cells[0].textContent.toLowerCase();
        const rowCategory = cells[1].textContent.toLowerCase();

        const matchesSearch = name.includes(searchText);
        const matchesCategory = category === '' || rowCategory === category;

        row.style.display = matchesSearch && matchesCategory ? '' : 'none';
      });
    }

    searchInput.addEventListener('input', filterTable);
    categoryFilter.addEventListener('change', filterTable);
})();