
import { register } from '../../lib/usersApi';

export const load: PageLoad = ({ fetch }) => {
  return {
    register: async (email: string, password: string) => {
      try {
        const response = await register(email, password);
        return { success: true, data: response };
      } catch (error) {
        return { success: false, error: error.message };
      }
    }
  };
};