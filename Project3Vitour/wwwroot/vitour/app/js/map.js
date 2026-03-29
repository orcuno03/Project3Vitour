if (document.getElementById('map')) {
    mapboxgl.accessToken = '';
    const map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/light-v11',
        center: [-0.108968, 51.492933],
        zoom: 14
    });

    const geojson = {
        type: 'FeatureCollection',
        features: [
            {
                type: 'Feature',
                geometry: {
                    type: 'Point',
                    coordinates: [-0.108968, 51.492933]
                }
            }
        ]
    };

    for (const feature of geojson.features) {
        const el = document.createElement('div');
        el.className = 'marker';
        new mapboxgl.Marker(el).setLngLat(feature.geometry.coordinates).addTo(map);
    }
}

if (document.getElementById('map2')) {
    mapboxgl.accessToken = '';
    const map2 = new mapboxgl.Map({
        container: 'map2',
        style: 'mapbox://styles/mapbox/light-v11',
        center: [-0.108968, 51.492933],
        zoom: 14
    });

    const geojson1 = {
        type: 'FeatureCollection',
        features: [
            {
                type: 'Feature',
                geometry: {
                    type: 'Point',
                    coordinates: [-0.108968, 51.492933]
                }
            }
        ]
    };

    for (const feature of geojson1.features) {
        const el = document.createElement('div');
        el.className = 'marker';
        new mapboxgl.Marker(el).setLngLat(feature.geometry.coordinates).addTo(map2);
    }
}

if (document.getElementById('map3')) {
    mapboxgl.accessToken = '';
    const map3 = new mapboxgl.Map({
        container: 'map3',
        style: 'mapbox://styles/mapbox/light-v11',
        center: [-0.108968, 51.492933],
        zoom: 14
    });

    const geojson2 = {
        type: 'FeatureCollection',
        features: [
            {
                type: 'Feature',
                geometry: {
                    type: 'Point',
                    coordinates: [-0.108968, 51.492933]
                }
            }
        ]
    };

    for (const feature of geojson2.features) {
        const el = document.createElement('div');
        el.className = 'marker';
        new mapboxgl.Marker(el).setLngLat(feature.geometry.coordinates).addTo(map3);
    }
}