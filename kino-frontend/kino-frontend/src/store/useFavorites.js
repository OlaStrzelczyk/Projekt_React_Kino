import { create } from 'zustand';

export const useFavoritesStore = create((set) => ({
  favorites: [],
  addFavorite: (movie) =>
    set((state) => {
      const exists = state.favorites.find((m) => m.id === movie.id);
      if (exists) return state;
      return { favorites: [...state.favorites, movie] };
    }),
  removeFavorite: (id) =>
    set((state) => ({
      favorites: state.favorites.filter((m) => m.id !== id),
    })),
}));