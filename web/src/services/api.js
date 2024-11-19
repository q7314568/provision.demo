const apiBaseUrl = import.meta.env.VITE_API_BASE_URL;

export async function getMonthlyRevenueSummary(query) {
  try {
    const response = await fetch(`${apiBaseUrl}/MonthlyRevenue/query`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(query),
    });

    if (!response.ok) {
      throw new Error('Network response was not ok ' + response.statusText);
    }

    return await response.json();
  } catch (error) {
    console.error('There was a problem with the fetch operation:', error);
    throw error;
  }
}

export async function addMonthlyRevenue(data) {
  try {
    const response = await fetch(`${apiBaseUrl}/MonthlyRevenue/add`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error('Network response was not ok ' + response.statusText);
    }

    return await response.json();
  } catch (error) {
    console.error('There was a problem with the fetch operation:', error);
    throw error;
  }
}
