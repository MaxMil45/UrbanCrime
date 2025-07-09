import requests
import sys

def postcode_to_latlng(postcode):
    url = f"https://api.postcodes.io/postcodes/{postcode}"
    res = requests.get(url)
    if res.status_code != 200:
        raise ValueError("Invalid postcode or postcode service unavailable.")
    data = res.json()
    return data['result']['latitude'], data['result']['longitude']

def get_crimes(lat, lng, date=None):
    url = "https://data.police.uk/api/crimes-street/all-crime"
    params = {"lat": lat, "lng": lng}
    if date:
        params["date"] = date  # format: yyyy-mm
    res = requests.get(url, params=params)
    if res.status_code != 200:
        raise RuntimeError("Could not fetch crime data.")
    return res.json()

def main():
    if len(sys.argv) < 2:
        print("Usage: python fetch_crime.py <postcode> [yyyy-mm]")
        sys.exit(1)

    postcode = sys.argv[1]
    date = sys.argv[2] if len(sys.argv) > 2 else None

    try:
        lat, lng = postcode_to_latlng(postcode)
        crimes = get_crimes(lat, lng, date)
        print(f"\nTop 5 crimes for postcode {postcode}:\n")
        for crime in crimes[:5]:
            print(f"{crime['category']} at {crime['location']['street']['name']}")
    except Exception as e:
        print("Error:", e)

if __name__ == "__main__":
    main()
